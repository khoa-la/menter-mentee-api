using System;

namespace _2MC.BusinessTier.Commons
{
    public class StringAttribute : Attribute
    {
    }
    
    public class SkipAttribute : Attribute
    {
    }
    
    public class BooleanAttribute : Attribute
    {
    }
    public class IntAttribute : Attribute
    {
    }
    public class ChildAttribute : Attribute
    {
    }
    
    public class DateRangeAttribute : Attribute
    {
    }

    public class SortAttribute : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HiddenParamsAttribute : Attribute
    {
        public string Params { get; set; }

        public HiddenParamsAttribute(string parameters)
        {
            this.Params = parameters;
        }
    }
}