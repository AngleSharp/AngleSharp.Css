namespace AngleSharp.Css.Tests.Parsing
{
    using AngleSharp.Css.Parser;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class StyleSheet
    {
        [Test]
        public async Task ParseEmptySheet_Issue42()
        {
            var sheetCode = "";
            var parser = new CssParser();
            var sheet = await parser.ParseStyleSheetAsync(sheetCode);
            Assert.IsNotNull(sheet);
        }

        [Test]
        public async Task ParseStyleSheetAndBack_Issue82()
        {
            var css = @"
    .root {
        position: absolute;
        top: 0;
        left: 0;
        width: calc(100% - 10px);
        height: calc(100% - 10px);
        padding: 5px;
    }

    .container {
        width: 100%;
        height: 100%;
        display: grid;
        grid-template-columns: 1fr 1fr;
        grid-template-rows: 240px calc(100% - 360px) 100px;
        grid-template-areas: 'header info' 'quantity info' 'footerOk footerCancel';
        row-gap: 10px;
        font-size: 26px;
    }

    .destination {
        grid-area: header;
        width: calc(100% - 20px);
        height: calc(100% - 20px);
        margin: 10px;
        display: grid;
        grid-template-columns: auto auto auto;
        grid-template-rows: 150px 50px;
        grid-template-areas:
            ""sourcerp image destrp""
            ""sourcele image destle"";
    }

    .quantitygrid {
        width: 100%;
        height: 100%;
        display: grid;
        grid-template-columns: auto auto auto;
        grid-template-rows: 150px 100px;
        grid-template-areas: 'minus number plus';
    }

    .headergrid {
        width: 100%;
        height: 100%;
        display: grid;
        grid-template-columns: 1fr 1fr 1fr;
        grid-template-rows: 1fr 1fr;
        grid-template-areas: 'sourceplace space destinationplace' 'sourcelc space destinationlc';
    }

    #info td + td {
        font-size: 24px;
    }

    input {
        animation-duration: 2s;
        font-family: inherit;
        background-color: white;
    }

    td {
        padding: 5px;
    }

    paper-button {
        padding: 0;
        background-color: var(--kx-dark);
        box-sizing: border-box;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        /*font-weight: bolder;*/
    }

    @keyframes errorAnimation {
        0% {
            background-color: white;
        }

        1% {
            background-color: red;
        }

        100% {
            background-color: white;
        }
    }

    input:invalid {
        color: red;
    }
";
            var parser = new CssParser();
            var formatter = new MinifyStyleFormatter();
            var ss = await parser.ParseStyleSheetAsync(css);
            var newCss = ss.ToCss(formatter);
            Assert.IsNotNull(newCss);
        }
    }
}
