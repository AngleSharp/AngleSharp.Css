#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using static CssConstructionFunctions;

    /// <summary>
    /// The registrations of a document are kept until something they depend on changes,
    /// which these tests change one at a time.
    /// </summary>
    [TestFixture]
    public class PropertyRegistrationCacheTests
    {
        private const String Length = "@property --len { syntax: \"<length>\"; inherits: false; initial-value: 0px; }";

        private static IDocument Document(String css) => ParseDocument($"<style>{css}</style><div id=target></div>");

        private static ICssStyleSheet Sheet(IDocument document) => (ICssStyleSheet)document.StyleSheets[0];

        private static ICssPropertyRule PropertyRule(IDocument document) => Sheet(document).Rules.OfType<ICssPropertyRule>().Single();

        [Test]
        public void SameRegistryWithoutChanges()
        {
            var document = Document(Length);
            var first = document.GetRegisteredProperty("--len");

            Assert.AreSame(first, document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void UnrelatedDomChangeKeepsTheRegistrations()
        {
            var document = Document(Length);
            document.GetRegisteredProperty("--len");
            document.Body.AppendChild(document.CreateElement("p"));

            Assert.AreEqual("<length>", document.GetRegisteredProperty("--len").Syntax);
        }

        [Test]
        public void InsertedAndRemovedRules()
        {
            var document = Document("");
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            Sheet(document).Insert(Length, 0);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));

            Sheet(document).RemoveAt(0);
            Assert.IsNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void RuleInsertedIntoAMediaRule()
        {
            var document = Document("@media all { }");
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            ((ICssMediaRule)Sheet(document).Rules[0]).Insert(Length, 0);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void ChangedDescriptors()
        {
            var document = Document(Length);
            Assert.AreEqual("<length>", document.GetRegisteredProperty("--len").Syntax);

            PropertyRule(document).SetProperty("syntax", "\"<color>\"");
            PropertyRule(document).SetProperty("initial-value", "red");
            Assert.AreEqual("<color>", document.GetRegisteredProperty("--len").Syntax);

            PropertyRule(document).RemoveProperty("inherits");
            Assert.IsNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void DescriptorChangedViaItsProperty()
        {
            var document = Document(Length);
            Assert.AreNotEqual("5px", document.GetRegisteredProperty("--len").InitialValue);

            PropertyRule(document).GetProperty("initial-value").Value = "5px";
            Assert.AreEqual("5px", document.GetRegisteredProperty("--len").InitialValue);
        }

        [Test]
        public void RemovedAndAddedStyleElements()
        {
            var document = Document(Length);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));

            var style = document.QuerySelector("style");
            style.Remove();
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            document.Head.AppendChild(style);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void ChangedStyleText()
        {
            var document = Document(Length);
            Assert.AreEqual("<length>", document.GetRegisteredProperty("--len").Syntax);

            document.QuerySelector("style").TextContent = Length.Replace("<length>", "<length-percentage>");
            Assert.AreEqual("<length-percentage>", document.GetRegisteredProperty("--len").Syntax);
        }

        [Test]
        public void ChangedMediaAttribute()
        {
            var document = Document(Length);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));

            document.QuerySelector("style").SetAttribute("media", "print");
            Assert.IsNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void DisabledStyleSheet()
        {
            var document = Document(Length);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));

            // a disabled sheet is not in the list of the document anymore
            var sheet = Sheet(document);
            sheet.IsDisabled = true;
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            sheet.IsDisabled = false;
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void AdoptedStyleSheets()
        {
            var document = Document("");
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            var sheet = new CssStyleSheet(document.DefaultView);
            sheet.ReplaceSync(Length);
            document.GetAdoptedStyleSheets().Add(sheet);
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));

            sheet.ReplaceSync("");
            Assert.IsNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void ChangedMediaRuleCondition()
        {
            var document = Document("@media print { " + Length + " }");
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            ((ICssMediaRule)Sheet(document).Rules[0]).Media.MediaText = "all";
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void ChangedDevice()
        {
            var device = new DefaultRenderDevice { DeviceWidth = 1280, DeviceHeight = 800, ViewPortWidth = 1280, ViewPortHeight = 800 };
            var context = BrowsingContext.New(Configuration.Default.WithCss().WithRenderDevice(device));
            var document = context.GetService<IHtmlParser>().ParseDocument("<style>@media (max-width: 600px) { " + Length + " }</style>");
            Assert.IsNull(document.GetRegisteredProperty("--len"));

            device.ViewPortWidth = 500;
            Assert.IsNotNull(document.GetRegisteredProperty("--len"));
        }

        [Test]
        public void ScriptRegistration()
        {
            var document = Document(Length);
            Assert.AreEqual(CssPropertyRegistrationOrigin.Rule, document.GetRegisteredProperty("--len").Origin);

            document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--len", Syntax = "*" });
            Assert.AreEqual(CssPropertyRegistrationOrigin.Script, document.GetRegisteredProperty("--len").Origin);
        }

        [Test]
        public void StyleMapSeesChanges()
        {
            var document = ParseDocument("<style></style><div id=target style=\"--len: 2px\"></div>");
            var map = document.GetElementById("target").GetAttributeStyleMap();
            Assert.IsInstanceOf<CssUnparsedValue>(map.Get("--len"));

            Sheet(document).Insert(Length, 0);
            Assert.IsInstanceOf<CssUnitValue>(map.Get("--len"));
        }
    }
}
