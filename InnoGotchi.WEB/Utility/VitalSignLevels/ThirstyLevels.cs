﻿namespace InnoGotchi.WEB.Utility.VitalSignLevels
{
    public class ThirstyLevels : VitalSignLevelValues
    {
        public ThirstyLevels() : base() { }

        public override Dictionary<int, VitalSignValue> SetLevels()
        {
            var result = new Dictionary<int, VitalSignValue>();
            result.Add(0, new VitalSignValue("Full", "badge-success"));
            result.Add(1, new VitalSignValue("Normal", "badge-warning"));
            result.Add(2, new VitalSignValue("Thirsty", "badge-danger"));
            result.Add(3, new VitalSignValue("Dead", "badge-secondary"));
            return result;
        }
    }
}
