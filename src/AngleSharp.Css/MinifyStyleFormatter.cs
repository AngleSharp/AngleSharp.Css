namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents the an CSS3 markup formatter with minimal code.
    /// </summary>
    public class MinifyStyleFormatter : IStyleFormatter, ICommentPreservingFormatter
    {
        #region Properties

        /// <summary>
        /// Gets or sets if comments should be preserved.
        /// </summary>
        public Boolean ShouldKeepComments { get; set; }

        /// <summary>
        /// Gets or sets if empty (zero-length) rules should be kept.
        /// </summary>
        public Boolean ShouldKeepEmptyRules { get; set; }

        #endregion

        #region Methods

        Boolean ICommentPreservingFormatter.PreserveComments => ShouldKeepComments;

        String IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
        {
            if (ShouldKeepEmptyRules || IsNotEmpty(rules))
            {
                var sb = StringBuilderPool.Obtain();

                using (var writer = new StringWriter(sb))
                {
                    foreach (var rule in rules)
                    {
                        rule.ToCss(writer, this);
                    }
                }

                return sb.ToPool();
            }

            return String.Empty;
        }

        String IStyleFormatter.BlockRules(IEnumerable<IStyleFormattable> rules)
        {
            if (ShouldKeepEmptyRules || IsNotEmpty(rules))
            {
                var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);

                using (var writer = new StringWriter(sb))
                {
                    foreach (var rule in rules)
                    {
                        rule.ToCss(writer, this);
                    }
                }

                return sb.Append(Symbols.CurlyBracketClose).ToPool();
            }

            return String.Empty;
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important) =>
            String.Concat(name, ":", String.Concat(value, important ? "!important" : String.Empty));

        String IStyleFormatter.BlockDeclarations(IEnumerable<IStyleFormattable> declarations)
        {
            if (ShouldKeepEmptyRules || declarations.OfType<ICssProperty>().Any())
            {
                var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);
                var hasProperty = false;

                using (var writer = new StringWriter(sb))
                {
                    foreach (var declaration in declarations)
                    {
                        declaration.ToCss(writer, this);

                        if (declaration is ICssProperty)
                        {
                            hasProperty = true;
                            writer.Write(Symbols.Semicolon);
                        }
                    }

                    if (hasProperty && sb.Length > 1 && sb[sb.Length - 1] == Symbols.Semicolon)
                    {
                        sb.Remove(sb.Length - 1, 1);
                    }
                }

                return sb.Append(Symbols.CurlyBracketClose).ToPool();
            }

            return String.Empty;
        }

        String IStyleFormatter.Rule(String name, String value) =>
            CssStyleFormatter.Instance.Rule(name, value);

        String IStyleFormatter.Rule(String name, String prelude, String rules) =>
            String.IsNullOrEmpty(rules) ? String.Empty : String.Concat(name, String.IsNullOrEmpty(prelude) ? String.Empty : " " + prelude, rules);

        String IStyleFormatter.Comment(String data) =>
            ShouldKeepComments ? CssStyleFormatter.Instance.Comment(data) : String.Empty;

        #endregion

        #region Helpers

        private Boolean IsNotEmpty(IEnumerable<IStyleFormattable> rules)
        {
            foreach (var rule in rules.OfType<ICssRule>())
            {
                switch (rule.Type)
                {
                    case CssRuleType.Document:
                    case CssRuleType.Supports:
                    case CssRuleType.Container:
                    case CssRuleType.Layer:
                    case CssRuleType.Scope:
                    case CssRuleType.StartingStyle:
                    case CssRuleType.Media:
                        if (IsNotEmpty(((ICssGroupingRule)rule).Rules))
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.Keyframes:
                        if (IsNotEmpty(((ICssKeyframesRule)rule).Rules))
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.FontFace:
                        if (((ICssFontFaceRule)rule).Style.Any())
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.Page:
                        if (((ICssPageRule)rule).Style.Any())
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.Style:
                        if (((ICssStyleRule)rule).Style.Any())
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.Keyframe:
                        if (((ICssKeyframeRule)rule).Style.Any())
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.PositionTry:
                        if (((ICssPositionTryRule)rule).Style.Any())
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.Property:
                    case CssRuleType.ViewTransition:
                    case CssRuleType.FontPaletteValues:
                    case CssRuleType.ColorProfile:
                        if (((ICssProperties)rule).Any())
                        {
                            return true;
                        }
                        break;
                    case CssRuleType.Comment:
                        if (ShouldKeepComments)
                        {
                            return true;
                        }
                        break;
                    default:
                        return true;
                }
            }

            return false;
        }

        #endregion
    }
}
