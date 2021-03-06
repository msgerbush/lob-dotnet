﻿using System;

namespace Lob
{
    public class Letter
    {
        public Letter() { }

        public Letter(
            string id,
            string description,
            object to,
            object from,
            bool color,
            string file,
            object mergeVariables,
            bool doubleSided,
            string addressPlacement,
            bool returnEnvelope,
            int? perforatedPage,
            string mailType,
            string extraService,
            DateTime sendDate,
            object metadata) {
            Id = id;
            Description = description;
            To = to;
            From = from;
            Color = color;
            File = file;
            MergeVariables = mergeVariables;
            DoubleSided = doubleSided;
            AddressPlacement = addressPlacement;
            ReturnEnvelope = returnEnvelope;
            PerforatedPage = perforatedPage;
            MailType = mailType;
            ExtraService = extraService;
            SendDate = sendDate;
            Metadata = metadata;
        }

        public string Id { get; protected set; }

        public string Description { get; protected set; }

        public object To { get; protected set; }

        public object From { get; protected set; }

        public bool Color { get; protected set; }

        public string File { get; protected set; }

        public object MergeVariables { get; protected set; }

        public bool DoubleSided { get; protected set; }

        public string AddressPlacement { get; protected set; }

        public bool ReturnEnvelope { get; protected set; }

        public int? PerforatedPage { get; protected set; }

        public string MailType { get; protected set; }

        public string ExtraService { get; protected set; }

        public DateTime SendDate { get; protected set; }

        public object Metadata { get; protected set; }
    }
}
