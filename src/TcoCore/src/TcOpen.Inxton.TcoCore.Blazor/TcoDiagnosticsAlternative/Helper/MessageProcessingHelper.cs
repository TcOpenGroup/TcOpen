﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

using TcoCore;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Helper
{
    class MessageProcessingHelper 
    {
        // not sure, why this has to be so high, otherwise the Message get not Acknowledged first Button Push
        static int delayForSettingPinnedFalse = 500;

        public MessageProcessingHelper()
        {
        }

        public async Task<List<PlainTcoMessage>> GetStillPinnedMsgsAfterSettingItFalse(List<PlainTcoMessage> messages)
        {
            if (messages == null || !messages.Any())
            {
                return new List<PlainTcoMessage>();
            }

            var tasks = messages.Select(ProcessMessageAsync).ToList();

            await Task.WhenAll(tasks);

            return tasks.Where(t => t.Result != null).Select(t => t.Result).ToList();
        }

        public async Task<PlainTcoMessage> GetStillPinnedSingleMsgAfterSettingItFalse(PlainTcoMessage message)
        {
            if (message == null)
            {
                return null;
            }

            return await ProcessMessageAsync(message);
        }

        public int CalculateDepth(MongoDbLogItem msg)
        {
            if (string.IsNullOrEmpty(msg.Properties.sender.Payload.ParentSymbol))
            {
                return 1;
            }

            int depth = msg.Properties.sender.Payload.ParentSymbol.Split('.').Length;
            return depth;
        }

        private async Task<PlainTcoMessage> ProcessMessageAsync(PlainTcoMessage message)
        {
            if (message == null)
            {
                return null;
            }

            message.OnlinerMessage.Pinned.Cyclic = false;

            await Task.Delay(delayForSettingPinnedFalse);

            if (message.OnlinerMessage.IsActive)
            {
                return message;
            }

            return null;
        }

        public static DateTime AdjustForDaylightSavingTime(DateTime date)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            if (localZone.IsDaylightSavingTime(date))
            {
                // Summertime
                return date.AddHours(2);
            }
            else
            {
                // Wintertime
                return date.AddHours(1);
            }
        }
    }
}