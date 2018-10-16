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
        [Terms("I3")]
        IntelCoreI3, IntelCoreI5, IntelCorei7, IntemCodeI9,
        [Terms("AMD")]
        ADMDual, ARM
    }

    [Serializable]
    public class FormFlowDemo
    {
        [Optional]
        [Describe(description: "Company", title: "Laptop Brand", subTitle: "There are meny brands available")]
        public LaptopBrand? LaptopBrand { get; set; }
        public LaptopType? LaptopType { get; set; }

        [Template(TemplateUsage.EnumSelectOne, "Select preferable {&}: {||}", ChoiceStyle=ChoiceStyleOptions.PerLine )]
        public LaptopOS? LaptopOS { get; set; }
        public LaptopProcessor? Processor { get; set; }

        [Describe("touch screen devices")]
        [Template(TemplateUsage.Bool,  "Do you prefer {&}? {||}", ChoiceStyle = ChoiceStyleOptions.Inline)]
        public bool? RequireTouch { get; set; }

        [Pattern(@"^\d\d{3,4}(-{0,1}| {0,1})\d{4}$")]
        public string UserMobileNo { get; set; }

        [Numeric(2, 12)]
        [Describe(description: "Minimum capacity of RAM")]
        [Template(TemplateUsage.NotUnderstood, "Unable to understood")]
        public int? MinimumRamSize { get; set; }

        public static IForm<FormFlowDemo> GetForm()
        {
            OnCompletionAsyncDelegate<FormFlowDemo> onFormCompletion = async (context, state) =>
            {
                await context.PostAsync(@"We have noted the configuration that you require. We will inform as we finish sending it");
            };

            return new FormBuilder<FormFlowDemo>()
                .Message("Wellcome to laptop suggestion bot")
                .Field(nameof(Processor))
                .Confirm(async(state) =>
                {
                    int price = 0;
                    switch (state.Processor)
                    {
                        case LaptopProcessor.IntelCoreI3: price = 200; break;
                        case LaptopProcessor.ADMDual: price = 300; break;
                        default: price = 500; break;
                    }
                    return new PromptAttribute($"Minimum price for this process will be {price}. Is it okay?");
                })
                .Field(nameof(UserMobileNo), validate: async(state, response) => {
                    var validatoinResult = new ValidateResult { IsValid = true, Value = response };
                    if((response as string) == "993710530")
                    {
                        validatoinResult.IsValid = false;
                        validatoinResult.Feedback = "Number not allowed";
                    }
                    return validatoinResult;
                })
                .Field(nameof(LaptopBrand))
                .AddRemainingFields()
                .Confirm("Your choosen processor is {Processor} and your phone is {UserMobileNo}")
                .OnCompletion(onFormCompletion)
                .Build();
        }
    }
}