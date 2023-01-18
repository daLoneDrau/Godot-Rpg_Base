using Godot;
using System;

namespace Base.Resources.EnumeratedTypes
{
    public enum Gender
    {
        MALE, FEMALE, NEUTRAL
    }
    static class GenderExtensions
    {
        public static int[] RollRange(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return new int[] { 1, 50 };
                case Gender.FEMALE:
                    return new int[] { 51, 100 };
                case Gender.NEUTRAL:
                    return new int[] { 101, 102 };
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
        public static string Title(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return "Male";
                case Gender.FEMALE:
                    return "Female";
                case Gender.NEUTRAL:
                    return "It";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
        public static string ChildRelation(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return "son";
                case Gender.FEMALE:
                    return "daughter";
                case Gender.NEUTRAL:
                    return "offspring";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
        public static string Objective(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return "him";
                case Gender.FEMALE:
                    return "her";
                case Gender.NEUTRAL:
                    return "it";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
        public static string Possessive(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return "his";
                case Gender.FEMALE:
                    return "her";
                case Gender.NEUTRAL:
                    return "its";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
        public static string PossessiveObjective(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return "his";
                case Gender.FEMALE:
                    return "hers";
                case Gender.NEUTRAL:
                    return "theirs";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
        public static string Pronoun(this Gender gender)
        {
            switch (gender)
            {
                case Gender.MALE:
                    return "he";
                case Gender.FEMALE:
                    return "she";
                case Gender.NEUTRAL:
                    return "it";
                default: throw new ArgumentOutOfRangeException("Gender");
            }
        }
    }
}
