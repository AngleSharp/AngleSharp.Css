#nullable disable
namespace AngleSharp.Css.Tests.Styling
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.TypedOm;
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using static CssConstructionFunctions;

    /// <summary>
    /// Custom properties registered with CSS.registerProperty() (CSS Properties and Values API 1, 4).
    /// </summary>
    [TestFixture]
    public class RegisterPropertyTests
    {
        private static PropertyDefinition Color(String name = "--c", Boolean inherits = false) => new()
        {
            Name = name,
            Syntax = "<color>",
            Inherits = inherits,
            InitialValue = "#c0ffee",
        };

        [Test]
        public void RegisteredPropertyHasTheInitialValue()
        {
            var document = ParseDocument("<div id=target style=\"color: var(--c)\"></div>");
            document.DefaultView.RegisterProperty(Color());
            var style = document.GetElementById("target").ComputeCurrentStyle();

            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("--c"));
            Assert.AreEqual("rgba(192, 255, 238, 1)", style.GetPropertyValue("color"));
        }

        [Test]
        public void RegisteredPropertyValidatesItsValues()
        {
            var document = ParseDocument("<div id=a style=\"--c: red\"></div><div id=b style=\"--c: 10px\"></div>");
            document.DefaultView.RegisterProperty(Color());

            Assert.AreEqual("rgba(255, 0, 0, 1)", document.GetElementById("a").ComputeCurrentStyle().GetPropertyValue("--c"));
            Assert.AreEqual("rgba(192, 255, 238, 1)", document.GetElementById("b").ComputeCurrentStyle().GetPropertyValue("--c"));
        }

        [Test]
        public void RegistrationAppliesToStylesComputedAfterwards()
        {
            var document = ParseDocument("<div id=parent style=\"--c: red\"><span id=target></span></div>");
            var target = document.GetElementById("target");

            Assert.AreEqual("red", target.ComputeCurrentStyle().GetPropertyValue("--c"));
            document.DefaultView.RegisterProperty(Color(inherits: false));
            Assert.AreEqual("rgba(192, 255, 238, 1)", target.ComputeCurrentStyle().GetPropertyValue("--c"));
        }

        [Test]
        public void InheritedRegistration()
        {
            var document = ParseDocument("<div id=parent style=\"--c: red\"><span id=target></span></div>");
            document.DefaultView.RegisterProperty(Color(inherits: true));
            Assert.AreEqual("rgba(255, 0, 0, 1)", document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--c"));
        }

        [Test]
        public void ScriptRegistrationWinsOverPropertyRule()
        {
            var document = ParseDocument("<style>@property --c { syntax: \"<length>\"; inherits: false; initial-value: 1px; }</style><div id=target style=\"--c: blue\"></div>");
            document.DefaultView.RegisterProperty(Color());
            Assert.AreEqual("rgba(0, 0, 255, 1)", document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--c"));
        }

        [Test]
        public void PropertyRuleDoesNotPreventARegistration()
        {
            var document = ParseDocument("<style>@property --c { syntax: \"<length>\"; inherits: false; initial-value: 1px; }</style>");
            Assert.DoesNotThrow(() => document.DefaultView.RegisterProperty(Color()));
        }

        [Test]
        public void RegistrationIsPerDocument()
        {
            var first = ParseDocument("<div id=target style=\"--c: 10px\"></div>");
            var second = ParseDocument("<div id=target style=\"--c: 10px\"></div>");
            first.DefaultView.RegisterProperty(Color());

            Assert.AreEqual("rgba(192, 255, 238, 1)", first.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--c"));
            Assert.AreEqual("10px", second.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--c"));
            Assert.DoesNotThrow(() => second.DefaultView.RegisterProperty(Color()));
        }

        [Test]
        public void RegisteringANameTwiceIsAnInvalidModification()
        {
            var document = ParseDocument("");
            document.DefaultView.RegisterProperty(Color());
            var ex = Assert.Throws<DomException>(() => document.DefaultView.RegisterProperty(Color()));
            Assert.AreEqual((Int32)DomError.InvalidModification, ex.Code);
        }

        [Test]
        public void DuplicateIsReportedBeforeAnInvalidDefinition()
        {
            var document = ParseDocument("");
            document.DefaultView.RegisterProperty(Color());
            var ex = Assert.Throws<DomException>(() => document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--c", Syntax = "<foo>" }));
            Assert.AreEqual((Int32)DomError.InvalidModification, ex.Code);
        }

        [TestCase("c", "<color>", "red")]
        [TestCase("--", "<color>", "red")]
        [TestCase("--c", "<foo>", "red")]
        [TestCase("--c", "", "red")]
        [TestCase("--c", "<color>", null)]
        [TestCase("--c", "<color>", "10px")]
        [TestCase("--c", "<length>", "2em")]
        [TestCase("--c", "<length>", "var(--x)")]
        public void InvalidDefinitionsAreSyntaxErrors(String name, String syntax, String initialValue)
        {
            var document = ParseDocument("");
            var definition = new PropertyDefinition { Name = name, Syntax = syntax, InitialValue = initialValue };
            var ex = Assert.Throws<DomException>(() => document.DefaultView.RegisterProperty(definition));
            Assert.AreEqual((Int32)DomError.Syntax, ex.Code);
        }

        [Test]
        public void FailedRegistrationRegistersNothing()
        {
            var document = ParseDocument("");
            Assert.Throws<DomException>(() => document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--c", Syntax = "<color>" }));
            Assert.DoesNotThrow(() => document.DefaultView.RegisterProperty(Color()));
        }

        [Test]
        public void UniversalSyntaxIsTheDefault()
        {
            var document = ParseDocument("<div id=parent style=\"--u: foo\"><span id=target></span></div>");
            document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--u" });

            Assert.AreEqual("foo", document.GetElementById("parent").ComputeCurrentStyle().GetPropertyValue("--u"));
            Assert.AreEqual(String.Empty, document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--u"));
        }

        [Test]
        public void RegistrationViaTheDocument()
        {
            var document = ParseDocument("<div id=target style=\"--len: 1in\"></div>");
            document.RegisterProperty(new PropertyDefinition { Name = "--len", Syntax = "<length>", InitialValue = "0px" });
            Assert.AreEqual("96px", document.GetElementById("target").ComputeCurrentStyle().GetPropertyValue("--len"));
        }

        [Test]
        public void TypedOmGivesTheTypedValue()
        {
            var document = ParseDocument("<div id=target style=\"font-size: 10px; --len: 3em\"></div>");
            document.DefaultView.RegisterProperty(new PropertyDefinition { Name = "--len", Syntax = "<length>", InitialValue = "0px" });
            var value = (CssUnitValue)document.GetElementById("target").ComputedStyleMap().Get("--len");

            Assert.AreEqual(30.0, value.Value);
            Assert.AreEqual("px", value.Unit);
        }

        [Test]
        public void RenderTreeUsesTheRegistration()
        {
            var document = ParseDocument("<div id=target style=\"--c: 10px; color: var(--c)\"></div>");
            document.DefaultView.RegisterProperty(Color());
            var tree = document.DefaultView.Render();
            var node = FindNode(tree, document.GetElementById("target"));

            Assert.IsNotNull(node);
            Assert.AreEqual("rgba(192, 255, 238, 1)", node.ComputedStyle.GetPropertyValue("color"));
        }

        private static AngleSharp.Css.RenderTree.ElementRenderNode FindNode(AngleSharp.Css.RenderTree.IRenderNode node, IElement element)
        {
            if (node is AngleSharp.Css.RenderTree.ElementRenderNode elementNode && elementNode.Ref == element)
            {
                return elementNode;
            }

            foreach (var child in node.Children)
            {
                var found = FindNode(child, element);

                if (found is not null)
                {
                    return found;
                }
            }

            return null;
        }
    }
}
