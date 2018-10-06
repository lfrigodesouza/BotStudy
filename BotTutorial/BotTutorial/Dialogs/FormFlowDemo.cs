using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;

namespace BotTutorial.Dialogs
{

    public enum LaptopType
    {
        Laptop, Gaming, Ultrabook, Notebook
    }

    public enum LaptopBrand
    {
        HP, Dell, Acer, Microsoft
    }

    public enum LaptopOS
    {
        Win10, Linux, iOS, Win8
    }

    public enum LaptopProcessor
    {
        [Describe(description: "Intel Core I3")]
        IntelCoreI3, IntelCoreI5, IntelCorei7, IntemCodeI9, ADMDual, ARM
    }

    [Serializable]
    public class FormFlowDemo
    {
        [Optional]
        [Describe(description: "Company", title: "Laptop Brand", subTitle: "There are meny brands available")]
        public LaptopBrand? LaptopBrand { get; set; }
        public LaptopType? LaptopType { get; set; }
        public LaptopOS? LaptopOS { get; set; }
        public LaptopProcessor? LaptopProcessor { get; set; }

        public bool? RequireTouch { get; set; }

        [Pattern(@"^\d\d{3,4}(-{0,1}| {0,1})\d{4}$")]
        public string UserMobileNo { get; set; }

        [Numeric(2, 12)]
        [Describe(description: "Minimum capacity of RAM")]
        public int? MinimumRamSize { get; set; }

        public static IForm<FormFlowDemo> GetForm()
        {
            return new FormBuilder<FormFlowDemo>()
                .Message("Wellcome to laptop suggestion bot")
                .Build();
        }
    }
}