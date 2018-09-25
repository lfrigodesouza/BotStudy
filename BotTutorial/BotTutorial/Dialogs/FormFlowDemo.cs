using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Dialogs;

namespace BotTutorial.Dialogs
{
    public enum LaptopBrand
    {
        HP, Dell, Acer, Microsoft
    }
    public enum LaptopType
    {
        Laptop, Gaming, Ultrabook, Notebook
    }

    public enum LaptopOS
    {
        Win10, Linux, iOS, Win8
    }

    public enum LaptopProcessor
    {
        IntelCoreI3, IntelCoreI5, IntelCorei7, IntemCodeI9, ADMDual, ARM
    }

    [Serializable]
    public class FormFlowDemo
    {
        public LaptopBrand? LaptopBrand { get; set; }
        public LaptopType? LaptopType { get; set; }
        public LaptopOS? LaptopOS { get; set; }
        public LaptopProcessor? LaptopProcessor { get; set; }

        public bool? RequireTouch { get; set; }

        public int? MinimumRAMSize { get; set; }

        public static IForm<FormFlowDemo> GetForm()
        {
            return new FormBuilder<FormFlowDemo>()
                .Message("Wellcome to laptop suggestion bot")
                .Build();
        }
    }
}