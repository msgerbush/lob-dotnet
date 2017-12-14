using System;

namespace Lob
{
    public class NewLetter
    {
        public NewLetter(
            object to,
            object from,
            string file,
            int? perforatedPage = null,
            string description = null,
            bool color = true,
            object mergeVariables = null,
            bool doubleSided = false,
            string addressPlacement = "top_first_page",
            bool returnEnvelope = false,
            string mailType = "usps_first_class",
            string extraService = null,
            DateTime? sendDate = null,
            object metadata = null
            )
        {
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

        public string Description { get; set; }

        public object To { get; set; }

        public object From { get; set; }

        public bool Color { get; set; }

        public string File { get; set; }

        public object MergeVariables { get; set; }

        public bool DoubleSided { get; set; }

        public string AddressPlacement { get; set; }

        public bool ReturnEnvelope { get; set; }

        public int? PerforatedPage { get; set; }

        public string MailType { get; set; }

        public string ExtraService { get; set; }

        public DateTime? SendDate { get; set; }

        public object Metadata { get; set; }
    }
}