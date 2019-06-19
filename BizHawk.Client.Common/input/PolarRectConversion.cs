using System;

namespace BizHawk.Client.Common
{
	public static class PolarRectConversion
	{
		/// <param name="θ">angle in degrees</param>
		/// <returns>rectangular (Cartesian) coordinates (x, y)</returns>
		/// <seealso cref="RectToPolarLookup"/>
		public static Tuple<short, short> PolarToRectLookup(ushort r, ushort θ) => new Tuple<short, short>(PolarRectConversionData._rθ2x.Value[r, θ], PolarRectConversionData._rθ2y.Value[r, θ]);

		/// <returns>polar coordinates (r, θ) where θ is in degrees</returns>
		/// <remarks>does intentional integer (byte) overflow so <paramref name="x"/> and <paramref name="y"/> can be used as array indices in <see cref="TranslatedRectToPolarLookup"/></remarks>
		/// <seealso cref="PolarToRectLookup"/>
		public static Tuple<ushort, ushort> RectToPolarLookup(sbyte x, sbyte y) => unchecked (TranslatedRectToPolarLookup((byte) x, (byte) y));

		private static Tuple<ushort, ushort> TranslatedRectToPolarLookup(byte x, byte y) => new Tuple<ushort, ushort>(PolarRectConversionData._xy2r.Value[x, y], PolarRectConversionData._xy2θ.Value[x, y]);
	}
}