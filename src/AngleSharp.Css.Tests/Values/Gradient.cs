namespace AngleSharp.Css.Tests.Values
{
    using AngleSharp.Css.Converters;
    using AngleSharp.Css.Values;
    using NUnit.Framework;
    using System.Linq;
    using static CssConstructionFunctions;
    using static ValueConverters;

    [TestFixture]
    public class GradientTests
    {
        [Test]
        public void InLinearGradient()
        {
            var source = "linear-gradient(135deg, red, blue)";
            var value = GradientConverter.Convert(source);
            Assert.IsNotNull(value);
        }

        [Test]
        public void InRadialGradient()
        {
            var source = "radial-gradient(ellipse farthest-corner at 45px 45px , #00FFFF, rgba(0, 0, 255, 0) 50%, #0000FF 95%)";
            var value = GradientConverter.Convert(source);
            Assert.IsNotNull(value);
        }

        [Test]
        public void BackgroundImageLinearGradientWithAngle()
        {
            var red = CssColorValue.Red;
            var blue = CssColorValue.Blue;
            var source = $"background-image: linear-gradient(135deg, {red.CssText}, {blue.CssText})";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsNotNull(gradient);
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Angle, Is.EqualTo(CssAngleValue.TripleHalfQuarter));
            Assert.That(gradient.Stops.Length, Is.EqualTo(2));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().First().Color, Is.EqualTo(red));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().Last().Color, Is.EqualTo(blue));

            Assert.That(property.CssText, Is.EqualTo(source));
        }

        [Test]
        public void BackgroundImageLinearGradientWithSide()
        {
            var source = "background-image: linear-gradient(to right, red, orange, yellow, green, blue, indigo, violet)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Angle, Is.EqualTo(CssAngleValue.Quarter));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(7));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColors.GetColor("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColors.GetColor("orange").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColors.GetColor("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[3]).Color, Is.EqualTo(CssColors.GetColor("green").Value));
            Assert.That(((CssGradientStopValue)stops[4]).Color, Is.EqualTo(CssColors.GetColor("blue").Value));
            Assert.That(((CssGradientStopValue)stops[5]).Color, Is.EqualTo(CssColors.GetColor("indigo").Value));
            Assert.That(((CssGradientStopValue)stops[6]).Color, Is.EqualTo(CssColors.GetColor("violet").Value));
        }

        [Test]
        public void BackgroundImageLinearGradientWithCornerAndRgba()
        {
            var source = "background-image: linear-gradient(to bottom right, red, rgba(255,0,0,0))";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Angle, Is.EqualTo(CssAngleValue.TripleHalfQuarter));
            Assert.That(gradient.Stops.Count(), Is.EqualTo(2));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().First().Color, Is.EqualTo(CssColorValue.Red));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().Last().Color, Is.EqualTo(CssColorValue.FromRgba(255, 0, 0, 0)));
        }

        [Test]
        public void BackgroundImageLinearGradientWithSideAndHsl()
        {
            var source = "background-image: linear-gradient(to bottom, hsl(0, 80%, 70%), #bada55)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Angle, Is.EqualTo(CssAngleValue.Half));
            Assert.That(gradient.Stops.Count(), Is.EqualTo(2));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().First().Color, Is.EqualTo(CssColorValue.FromHsl(0f, 0.8f, 0.7f)));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().Last().Color, Is.EqualTo(CssColorValue.FromHex("bada55")));
        }

        [Test]
        public void BackgroundImageLinearGradientNoAngle()
        {
            var source = "background-image: linear-gradient(yellow, blue 20%, #0f0)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Angle, Is.EqualTo(CssAngleValue.Half));
            Assert.That(gradient.Stops.Count(), Is.EqualTo(3));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().First().Color, Is.EqualTo(CssColors.GetColor("yellow").Value));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().Skip(1).First().Color, Is.EqualTo(CssColors.GetColor("blue").Value));
            Assert.That(gradient.Stops.OfType<CssGradientStopValue>().Skip(2).First().Color, Is.EqualTo(CssColorValue.FromRgb(0, 255, 0)));
        }

        [Test]
        public void BackgroundImageRadialGradientCircleFarthestCorner()
        {
            var source = "background-image: radial-gradient(circle farthest-corner at 45px 45px , #00FFFF 0%, rgba(0, 0, 255, 0) 50%, #0000FF 95%)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(45, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(45, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(true));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.FarthestCorner));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromRgb(0, 255, 255)));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromRgba(0, 0, 255, 0)));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromRgb(0, 0, 255)));
        }

        [Test]
        public void BackgroundImageRadialGradientEllipseFarthestCorner()
        {
            var source = "background-image: radial-gradient(ellipse farthest-corner at 470px 47px , #FFFF80 20%, rgba(204, 153, 153, 0.4) 30%, #E6E6FF 60%)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(470, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(47, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.FarthestCorner));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromRgb(0xFF, 0xFF, 0x80)));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromRgba(204, 153, 153, 0.4f)));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromRgb(0xE6, 0xE6, 0xFF)));
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestCornerWithPoint()
        {
            var source = "background-image: radial-gradient(farthest-corner at 45px 45px , #FF0000 0%, #0000FF 100%)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(45, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(45, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.FarthestCorner));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(2));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromRgb(255, 0, 0)));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromRgb(0, 0, 255)));
        }

        [Test]
        public void BackgroundImageRadialGradientSingleSize()
        {
            var source = "background-image: radial-gradient(16px at 60px 50% , #000000 0%, #000000 14px, rgba(0, 0, 0, 0.3) 18px, rgba(0, 0, 0, 0) 19px)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(60f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.IsCircle, Is.EqualTo(true));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.None));
            Assert.That(gradient.MajorRadius, Is.EqualTo(new CssLengthValue(16f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.MinorRadius, Is.EqualTo(CssLengthValue.Full));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(4));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromRgb(0, 0, 0)));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromRgb(0, 0, 0)));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromRgba(0, 0, 0, 0.3)));
            Assert.That(((CssGradientStopValue)stops[3]).Color, Is.EqualTo(CssColorValue.Transparent));
        }

        [Test]
        public void BackgroundImageRadialGradientCircle()
        {
            var source = "background-image: radial-gradient(circle, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.IsCircle, Is.EqualTo(true));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.None));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(2));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientOnlyGradientStops()
        {
            var source = "background-image: radial-gradient(yellow, green)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.None));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(2));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientEllipseAtCenter()
        {
            var source = "background-image: radial-gradient(ellipse at center, yellow 0%, green 100%)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.None));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(2));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestCornerWithoutPoint()
        {
            var source = "background-image: radial-gradient(farthest-corner, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.FarthestCorner));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(2));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientClosestSideWithPoint()
        {
            var source = "background-image: radial-gradient(closest-side at 20px 30px, red, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(20f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(30f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.ClosestSide));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientSizeAndPoint()
        {
            var source = "background-image: radial-gradient(20px 30px at 20px 30px, red, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(20f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(30f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.None));
            Assert.That(gradient.MajorRadius, Is.EqualTo(new CssLengthValue(20f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.MinorRadius, Is.EqualTo(new CssLengthValue(30f, CssLengthValue.Unit.Px)));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientClosestSideCircleShuffledWithPoint()
        {
            var source = "background-image: radial-gradient(closest-side circle at 20px 30px, red, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(20f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(30f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(true));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.ClosestSide));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestSideLeftBottom()
        {
            var source = "background-image: radial-gradient(farthest-side at left bottom, red, yellow 50px, green);";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.False);
            Assert.That(gradient.Position.X, Is.EqualTo(CssLengthValue.Zero));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Full));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.FarthestSide));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
        }

        [Test]
        public void BackgroundImageRepeatingLinearGradientRedBlue()
        {
            var source = "background-image: repeating-linear-gradient(red, blue 20px, red 40px)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.That(gradient.IsRepeating, Is.True);
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("blue").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
        }

        [Test]
        public void BackgroundImageRepeatingRadialGradientRedBlue()
        {
            var source = "background-image: repeating-radial-gradient(red, blue 20px, red 40px)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.True);
            Assert.That(gradient.Position.X, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.Position.Y, Is.EqualTo(CssLengthValue.Half));
            Assert.That(gradient.IsCircle, Is.EqualTo(false));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.None));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(3));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("blue").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
        }

        [Test]
        public void BackgroundImageRepeatingRadialGradientFunky()
        {
            var source = "background-image: repeating-radial-gradient(circle closest-side at 20px 30px, red, yellow, green 100%, yellow 150%, red 200%)";
            var property = ParseDeclaration(source);
            Assert.That(property.HasValue, Is.True);
            Assert.That(property.IsInitial, Is.False);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.That(value.Items.Length, Is.EqualTo(1));
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.That(gradient.IsRepeating, Is.True);
            Assert.That(gradient.Position.X, Is.EqualTo(new CssLengthValue(20f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.Position.Y, Is.EqualTo(new CssLengthValue(30f, CssLengthValue.Unit.Px)));
            Assert.That(gradient.IsCircle, Is.EqualTo(true));
            Assert.That(gradient.Mode, Is.EqualTo(CssRadialGradientValue.SizeMode.ClosestSide));
            var stops = gradient.Stops.ToArray();
            Assert.That(stops.Length, Is.EqualTo(5));
            Assert.That(((CssGradientStopValue)stops[0]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
            Assert.That(((CssGradientStopValue)stops[1]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[2]).Color, Is.EqualTo(CssColorValue.FromName("green").Value));
            Assert.That(((CssGradientStopValue)stops[3]).Color, Is.EqualTo(CssColorValue.FromName("yellow").Value));
            Assert.That(((CssGradientStopValue)stops[4]).Color, Is.EqualTo(CssColorValue.FromName("red").Value));
        }
    }
}
