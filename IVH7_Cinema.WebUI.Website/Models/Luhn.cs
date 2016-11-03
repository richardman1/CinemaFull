using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVH7_Cinema.WebUI.Website.Models {

    //http://rosettacode.org/wiki/Luhn_test_of_credit_card_numbers#C.23
    public static class Luhn {

        public static bool LuhnCheck(this string cardNumber) {
            return LuhnCheck(cardNumber.Select(c => c - '0').ToArray());
        }

        private static bool LuhnCheck(this int[] digits) {
            return GetCheckValue(digits) == 0;
        }

        private static int GetCheckValue(int[] digits) {
            return digits.Select((d, i) => i % 2 == digits.Length % 2 ? ((2 * d) % 10) + d / 5 : d).Sum() % 10;
        }

    }
}