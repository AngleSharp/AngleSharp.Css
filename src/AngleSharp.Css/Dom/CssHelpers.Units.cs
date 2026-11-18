namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Css.TypedOm;
    using System;

    /// <summary>
    /// The factory functions of numeric values (CSS Typed OM), e.g., CSS.px(10).
    /// https://www.w3.org/TR/css-typed-om-1/#numeric-factory
    /// </summary>
    public static partial class CssHelpers
    {
        /// <summary>
        /// Creates a plain number, e.g., CSS.number(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("number")]
        public static CssUnitValue Number(Double value) => new(value, "number");

        /// <summary>
        /// Creates a percentage, e.g., CSS.percent(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("percent")]
        public static CssUnitValue Percent(Double value) => new(value, "percent");

        /// <summary>
        /// Creates a value in em, e.g., CSS.em(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("em")]
        public static CssUnitValue Em(Double value) => new(value, "em");

        /// <summary>
        /// Creates a value in rem, e.g., CSS.rem(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("rem")]
        public static CssUnitValue Rem(Double value) => new(value, "rem");

        /// <summary>
        /// Creates a value in ex, e.g., CSS.ex(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("ex")]
        public static CssUnitValue Ex(Double value) => new(value, "ex");

        /// <summary>
        /// Creates a value in rex, e.g., CSS.rex(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("rex")]
        public static CssUnitValue Rex(Double value) => new(value, "rex");

        /// <summary>
        /// Creates a value in cap, e.g., CSS.cap(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cap")]
        public static CssUnitValue Cap(Double value) => new(value, "cap");

        /// <summary>
        /// Creates a value in rcap, e.g., CSS.rcap(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("rcap")]
        public static CssUnitValue Rcap(Double value) => new(value, "rcap");

        /// <summary>
        /// Creates a value in ch, e.g., CSS.ch(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("ch")]
        public static CssUnitValue Ch(Double value) => new(value, "ch");

        /// <summary>
        /// Creates a value in rch, e.g., CSS.rch(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("rch")]
        public static CssUnitValue Rch(Double value) => new(value, "rch");

        /// <summary>
        /// Creates a value in ic, e.g., CSS.ic(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("ic")]
        public static CssUnitValue Ic(Double value) => new(value, "ic");

        /// <summary>
        /// Creates a value in ric, e.g., CSS.ric(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("ric")]
        public static CssUnitValue Ric(Double value) => new(value, "ric");

        /// <summary>
        /// Creates a value in lh, e.g., CSS.lh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lh")]
        public static CssUnitValue Lh(Double value) => new(value, "lh");

        /// <summary>
        /// Creates a value in rlh, e.g., CSS.rlh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("rlh")]
        public static CssUnitValue Rlh(Double value) => new(value, "rlh");

        /// <summary>
        /// Creates a value in vw, e.g., CSS.vw(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("vw")]
        public static CssUnitValue Vw(Double value) => new(value, "vw");

        /// <summary>
        /// Creates a value in vh, e.g., CSS.vh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("vh")]
        public static CssUnitValue Vh(Double value) => new(value, "vh");

        /// <summary>
        /// Creates a value in vi, e.g., CSS.vi(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("vi")]
        public static CssUnitValue Vi(Double value) => new(value, "vi");

        /// <summary>
        /// Creates a value in vb, e.g., CSS.vb(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("vb")]
        public static CssUnitValue Vb(Double value) => new(value, "vb");

        /// <summary>
        /// Creates a value in vmin, e.g., CSS.vmin(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("vmin")]
        public static CssUnitValue Vmin(Double value) => new(value, "vmin");

        /// <summary>
        /// Creates a value in vmax, e.g., CSS.vmax(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("vmax")]
        public static CssUnitValue Vmax(Double value) => new(value, "vmax");

        /// <summary>
        /// Creates a value in svw, e.g., CSS.svw(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("svw")]
        public static CssUnitValue Svw(Double value) => new(value, "svw");

        /// <summary>
        /// Creates a value in svh, e.g., CSS.svh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("svh")]
        public static CssUnitValue Svh(Double value) => new(value, "svh");

        /// <summary>
        /// Creates a value in svi, e.g., CSS.svi(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("svi")]
        public static CssUnitValue Svi(Double value) => new(value, "svi");

        /// <summary>
        /// Creates a value in svb, e.g., CSS.svb(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("svb")]
        public static CssUnitValue Svb(Double value) => new(value, "svb");

        /// <summary>
        /// Creates a value in svmin, e.g., CSS.svmin(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("svmin")]
        public static CssUnitValue Svmin(Double value) => new(value, "svmin");

        /// <summary>
        /// Creates a value in svmax, e.g., CSS.svmax(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("svmax")]
        public static CssUnitValue Svmax(Double value) => new(value, "svmax");

        /// <summary>
        /// Creates a value in lvw, e.g., CSS.lvw(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lvw")]
        public static CssUnitValue Lvw(Double value) => new(value, "lvw");

        /// <summary>
        /// Creates a value in lvh, e.g., CSS.lvh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lvh")]
        public static CssUnitValue Lvh(Double value) => new(value, "lvh");

        /// <summary>
        /// Creates a value in lvi, e.g., CSS.lvi(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lvi")]
        public static CssUnitValue Lvi(Double value) => new(value, "lvi");

        /// <summary>
        /// Creates a value in lvb, e.g., CSS.lvb(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lvb")]
        public static CssUnitValue Lvb(Double value) => new(value, "lvb");

        /// <summary>
        /// Creates a value in lvmin, e.g., CSS.lvmin(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lvmin")]
        public static CssUnitValue Lvmin(Double value) => new(value, "lvmin");

        /// <summary>
        /// Creates a value in lvmax, e.g., CSS.lvmax(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("lvmax")]
        public static CssUnitValue Lvmax(Double value) => new(value, "lvmax");

        /// <summary>
        /// Creates a value in dvw, e.g., CSS.dvw(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dvw")]
        public static CssUnitValue Dvw(Double value) => new(value, "dvw");

        /// <summary>
        /// Creates a value in dvh, e.g., CSS.dvh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dvh")]
        public static CssUnitValue Dvh(Double value) => new(value, "dvh");

        /// <summary>
        /// Creates a value in dvi, e.g., CSS.dvi(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dvi")]
        public static CssUnitValue Dvi(Double value) => new(value, "dvi");

        /// <summary>
        /// Creates a value in dvb, e.g., CSS.dvb(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dvb")]
        public static CssUnitValue Dvb(Double value) => new(value, "dvb");

        /// <summary>
        /// Creates a value in dvmin, e.g., CSS.dvmin(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dvmin")]
        public static CssUnitValue Dvmin(Double value) => new(value, "dvmin");

        /// <summary>
        /// Creates a value in dvmax, e.g., CSS.dvmax(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dvmax")]
        public static CssUnitValue Dvmax(Double value) => new(value, "dvmax");

        /// <summary>
        /// Creates a value in cqw, e.g., CSS.cqw(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cqw")]
        public static CssUnitValue Cqw(Double value) => new(value, "cqw");

        /// <summary>
        /// Creates a value in cqh, e.g., CSS.cqh(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cqh")]
        public static CssUnitValue Cqh(Double value) => new(value, "cqh");

        /// <summary>
        /// Creates a value in cqi, e.g., CSS.cqi(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cqi")]
        public static CssUnitValue Cqi(Double value) => new(value, "cqi");

        /// <summary>
        /// Creates a value in cqb, e.g., CSS.cqb(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cqb")]
        public static CssUnitValue Cqb(Double value) => new(value, "cqb");

        /// <summary>
        /// Creates a value in cqmin, e.g., CSS.cqmin(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cqmin")]
        public static CssUnitValue Cqmin(Double value) => new(value, "cqmin");

        /// <summary>
        /// Creates a value in cqmax, e.g., CSS.cqmax(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cqmax")]
        public static CssUnitValue Cqmax(Double value) => new(value, "cqmax");

        /// <summary>
        /// Creates a value in cm, e.g., CSS.cm(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("cm")]
        public static CssUnitValue Cm(Double value) => new(value, "cm");

        /// <summary>
        /// Creates a value in mm, e.g., CSS.mm(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("mm")]
        public static CssUnitValue Mm(Double value) => new(value, "mm");

        /// <summary>
        /// Creates a value in Q, e.g., CSS.Q(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("Q")]
        public static CssUnitValue Q(Double value) => new(value, "q");

        /// <summary>
        /// Creates a value in in, e.g., CSS.in(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("in")]
        public static CssUnitValue In(Double value) => new(value, "in");

        /// <summary>
        /// Creates a value in pt, e.g., CSS.pt(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("pt")]
        public static CssUnitValue Pt(Double value) => new(value, "pt");

        /// <summary>
        /// Creates a value in pc, e.g., CSS.pc(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("pc")]
        public static CssUnitValue Pc(Double value) => new(value, "pc");

        /// <summary>
        /// Creates a value in px, e.g., CSS.px(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("px")]
        public static CssUnitValue Px(Double value) => new(value, "px");

        /// <summary>
        /// Creates a value in deg, e.g., CSS.deg(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("deg")]
        public static CssUnitValue Deg(Double value) => new(value, "deg");

        /// <summary>
        /// Creates a value in grad, e.g., CSS.grad(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("grad")]
        public static CssUnitValue Grad(Double value) => new(value, "grad");

        /// <summary>
        /// Creates a value in rad, e.g., CSS.rad(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("rad")]
        public static CssUnitValue Rad(Double value) => new(value, "rad");

        /// <summary>
        /// Creates a value in turn, e.g., CSS.turn(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("turn")]
        public static CssUnitValue Turn(Double value) => new(value, "turn");

        /// <summary>
        /// Creates a value in s, e.g., CSS.s(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("s")]
        public static CssUnitValue S(Double value) => new(value, "s");

        /// <summary>
        /// Creates a value in ms, e.g., CSS.ms(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("ms")]
        public static CssUnitValue Ms(Double value) => new(value, "ms");

        /// <summary>
        /// Creates a value in Hz, e.g., CSS.Hz(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("Hz")]
        public static CssUnitValue Hz(Double value) => new(value, "hz");

        /// <summary>
        /// Creates a value in kHz, e.g., CSS.kHz(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("kHz")]
        public static CssUnitValue KHz(Double value) => new(value, "khz");

        /// <summary>
        /// Creates a value in dpi, e.g., CSS.dpi(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dpi")]
        public static CssUnitValue Dpi(Double value) => new(value, "dpi");

        /// <summary>
        /// Creates a value in dpcm, e.g., CSS.dpcm(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dpcm")]
        public static CssUnitValue Dpcm(Double value) => new(value, "dpcm");

        /// <summary>
        /// Creates a value in dppx, e.g., CSS.dppx(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("dppx")]
        public static CssUnitValue Dppx(Double value) => new(value, "dppx");

        /// <summary>
        /// Creates a value in fr, e.g., CSS.fr(1).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The unit value.</returns>
        [DomName("fr")]
        public static CssUnitValue Fr(Double value) => new(value, "fr");
    }
}
