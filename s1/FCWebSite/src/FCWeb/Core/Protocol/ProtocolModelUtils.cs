namespace FCWeb.Core.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCCore.Abstractions.Bll.Components;
    using FCCore.Model;
    using ViewModels.Protocol;

    public class ProtocolModelUtils
    {
        private IGameFormatManager gameFormatManager;

        public ProtocolModelUtils(IGameFormatManager gameFormatManager)
        {
            this.gameFormatManager = gameFormatManager;
        }

        public void CalculateMinutes(ProtocolRecord record, EntityLinkProtocolViewModel model)
        {
            if (!record.ExtraTime)
            {
                model.minute = record.Minute;
                model.extraMinute = null;
                return;
            }

            model.minute = record.Minute.HasValue ? gameFormatManager.GetRoundTime(record.Minute.Value) : record.Minute;
            model.extraMinute = model.minute.HasValue ? record.Minute.Value - model.minute.Value : (int?)null;
        }

        public void CalculateMinutes(FakeProtocolEventViewModel record, EntityLinkProtocolViewModel model)
        {
            if (!record.extraTime)
            {
                model.minute = record.minute;
                model.extraMinute = null;
                return;
            }

            model.minute = record.minute.HasValue ? gameFormatManager.GetRoundTime(record.minute.Value) : record.minute;
            model.extraMinute = model.minute.HasValue ? record.minute.Value - model.minute.Value : (int?)null;
        }
    }
}
