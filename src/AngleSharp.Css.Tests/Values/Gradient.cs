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
            var red = Color.Red;
            var blue = Color.Blue;
            var source = $"background-image: linear-gradient(135deg, {red.CssText}, {blue.CssText})";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsNotNull(gradient);
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Angle.TripleHalfQuarter, gradient.Angle);
            Assert.AreEqual(2, gradient.Stops.Length);
            Assert.AreEqual(red, gradient.Stops.First().Color);
            Assert.AreEqual(blue, gradient.Stops.Last().Color);

            Assert.AreEqual(source, property.CssText);
        }

        [Test]
        public void BackgroundImageLinearGradientWithSide()
        {
            var source = "background-image: linear-gradient(to right, red, orange, yellow, green, blue, indigo, violet)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Angle.Quarter, gradient.Angle);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(7, stops.Length);
            Assert.AreEqual(CssColors.GetColor("red").Value, stops[0].Color);
            Assert.AreEqual(CssColors.GetColor("orange").Value, stops[1].Color);
            Assert.AreEqual(CssColors.GetColor("yellow").Value, stops[2].Color);
            Assert.AreEqual(CssColors.GetColor("green").Value, stops[3].Color);
            Assert.AreEqual(CssColors.GetColor("blue").Value, stops[4].Color);
            Assert.AreEqual(CssColors.GetColor("indigo").Value, stops[5].Color);
            Assert.AreEqual(CssColors.GetColor("violet").Value, stops[6].Color);
        }

        [Test]
        public void BackgroundImageLinearGradientWithCornerAndRgba()
        {
            var source = "background-image: linear-gradient(to bottom right, red, rgba(255,0,0,0))";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Angle.TripleHalfQuarter, gradient.Angle);
            Assert.AreEqual(2, gradient.Stops.Count());
            Assert.AreEqual(Color.Red, gradient.Stops.First().Color);
            Assert.AreEqual(Color.FromRgba(255, 0, 0, 0), gradient.Stops.Last().Color);
        }

        [Test]
        public void BackgroundImageLinearGradientWithSideAndHsl()
        {
            var source = "background-image: linear-gradient(to bottom, hsl(0, 80%, 70%), #bada55)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Angle.Half, gradient.Angle);
            Assert.AreEqual(2, gradient.Stops.Count());
            Assert.AreEqual(Color.FromHsl(0f, 0.8f, 0.7f), gradient.Stops.First().Color);
            Assert.AreEqual(Color.FromHex("bada55"), gradient.Stops.Last().Color);
        }

        [Test]
        public void BackgroundImageLinearGradientNoAngle()
        {
            var source = "background-image: linear-gradient(yellow, blue 20%, #0f0)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Angle.Half, gradient.Angle);
            Assert.AreEqual(3, gradient.Stops.Count());
            Assert.AreEqual(CssColors.GetColor("yellow").Value, gradient.Stops.First().Color);
            Assert.AreEqual(CssColors.GetColor("blue").Value, gradient.Stops.Skip(1).First().Color);
            Assert.AreEqual(Color.FromRgb(0, 255, 0), gradient.Stops.Skip(2).First().Color);
        }

        [Test]
        public void BackgroundImageRadialGradientCircleFarthestCorner()
        {
            var source = "background-image: radial-gradient(circle farthest-corner at 45px 45px , #00FFFF 0%, rgba(0, 0, 255, 0) 50%, #0000FF 95%)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(45, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(45, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(true, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.FarthestCorner, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromRgb(0, 255, 255), stops[0].Color);
            Assert.AreEqual(Color.FromRgba(0, 0, 255, 0), stops[1].Color);
            Assert.AreEqual(Color.FromRgb(0, 0, 255), stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientEllipseFarthestCorner()
        {
            var source = "background-image: radial-gradient(ellipse farthest-corner at 470px 47px , #FFFF80 20%, rgba(204, 153, 153, 0.4) 30%, #E6E6FF 60%)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(470, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(47, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.FarthestCorner, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromRgb(0xFF, 0xFF, 0x80), stops[0].Color);
            Assert.AreEqual(Color.FromRgba(204, 153, 153, 0.4f), stops[1].Color);
            Assert.AreEqual(Color.FromRgb(0xE6, 0xE6, 0xFF), stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestCornerWithPoint()
        {
            var source = "background-image: radial-gradient(farthest-corner at 45px 45px , #FF0000 0%, #0000FF 100%)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(45, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(45, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.FarthestCorner, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromRgb(255, 0, 0), stops[0].Color);
            Assert.AreEqual(Color.FromRgb(0, 0, 255), stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientSingleSize()
        {
            var source = "background-image: radial-gradient(16px at 60px 50% , #000000 0%, #000000 14px, rgba(0, 0, 0, 0.3) 18px, rgba(0, 0, 0, 0) 19px)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(60f, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(Length.Half, gradient.Position.Y);
            Assert.AreEqual(true, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.None, gradient.Mode);
            Assert.AreEqual(new Length(16f, Length.Unit.Px), gradient.MajorRadius);
            Assert.AreEqual(Length.Full, gradient.MinorRadius);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(4, stops.Length);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), stops[0].Color);
            Assert.AreEqual(Color.FromRgb(0, 0, 0), stops[1].Color);
            Assert.AreEqual(Color.FromRgba(0, 0, 0, 0.3), stops[2].Color);
            Assert.AreEqual(Color.Transparent, stops[3].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientCircle()
        {
            var source = "background-image: radial-gradient(circle, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Length.Half, gradient.Position.X);
            Assert.AreEqual(Length.Half, gradient.Position.Y);
            Assert.AreEqual(true, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.None, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientOnlyGradientStops()
        {
            var source = "background-image: radial-gradient(yellow, green)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Length.Half, gradient.Position.X);
            Assert.AreEqual(Length.Half, gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.None, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientEllipseAtCenter()
        {
            var source = "background-image: radial-gradient(ellipse at center, yellow 0%, green 100%)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Length.Half, gradient.Position.X);
            Assert.AreEqual(Length.Half, gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.None, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestCornerWithoutPoint()
        {
            var source = "background-image: radial-gradient(farthest-corner, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Length.Half, gradient.Position.X);
            Assert.AreEqual(Length.Half, gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.FarthestCorner, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(2, stops.Length);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[1].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientClosestSideWithPoint()
        {
            var source = "background-image: radial-gradient(closest-side at 20px 30px, red, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.ClosestSide, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientSizeAndPoint()
        {
            var source = "background-image: radial-gradient(20px 30px at 20px 30px, red, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.None, gradient.Mode);
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.MajorRadius);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.MinorRadius);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientClosestSideCircleShuffledWithPoint()
        {
            var source = "background-image: radial-gradient(closest-side circle at 20px 30px, red, yellow, green)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(true, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.ClosestSide, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRadialGradientFarthestSideLeftBottom()
        {
            var source = "background-image: radial-gradient(farthest-side at left bottom, red, yellow 50px, green);";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsFalse(gradient.IsRepeating);
            Assert.AreEqual(Length.Zero, gradient.Position.X);
            Assert.AreEqual(Length.Full, gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.FarthestSide, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRepeatingLinearGradientRedBlue()
        {
            var source = "background-image: repeating-linear-gradient(red, blue 20px, red 40px)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssLinearGradientValue;
            Assert.IsTrue(gradient.IsRepeating);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("blue").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("red").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRepeatingRadialGradientRedBlue()
        {
            var source = "background-image: repeating-radial-gradient(red, blue 20px, red 40px)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsTrue(gradient.IsRepeating);
            Assert.AreEqual(Length.Half, gradient.Position.X);
            Assert.AreEqual(Length.Half, gradient.Position.Y);
            Assert.AreEqual(false, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.None, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(3, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("blue").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("red").Value, stops[2].Color);
        }

        [Test]
        public void BackgroundImageRepeatingRadialGradientFunky()
        {
            var source = "background-image: repeating-radial-gradient(circle closest-side at 20px 30px, red, yellow, green 100%, yellow 150%, red 200%)";
            var property = ParseDeclaration(source);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInitial);
            var value = property.RawValue as CssListValue;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Items.Length);
            var gradient = value.Items[0] as CssRadialGradientValue;
            Assert.IsTrue(gradient.IsRepeating);
            Assert.AreEqual(new Length(20f, Length.Unit.Px), gradient.Position.X);
            Assert.AreEqual(new Length(30f, Length.Unit.Px), gradient.Position.Y);
            Assert.AreEqual(true, gradient.IsCircle);
            Assert.AreEqual(CssRadialGradientValue.SizeMode.ClosestSide, gradient.Mode);
            var stops = gradient.Stops.ToArray();
            Assert.AreEqual(5, stops.Length);
            Assert.AreEqual(Color.FromName("red").Value, stops[0].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[1].Color);
            Assert.AreEqual(Color.FromName("green").Value, stops[2].Color);
            Assert.AreEqual(Color.FromName("yellow").Value, stops[3].Color);
            Assert.AreEqual(Color.FromName("red").Value, stops[4].Color);
        }
    }
}
