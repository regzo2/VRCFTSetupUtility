﻿using System.Collections.Generic;
using System.Linq;

namespace VRCFaceTracking.Tools.Setup_Utility.Editor
{
    public class ParamData
    {
        public ParamData(string name, ParamMeta.ParameterType type, (string, float)[] steps, 
            List<MRBlendshapeSaveState> defaultValues, float defaultStep = 0)
        {
            Name = name;
            Type = type;
            AnimationSteps = new Dictionary<(string, float), List<MRBlendshapeSaveState>>();
            // Add an entry for each step
            foreach (var step in steps)
                AnimationSteps.Add(step, new List<MRBlendshapeSaveState>());
            
            // Add the default values
            DefaultStep = steps.First(s => s.Item2 == defaultStep);
            AnimationSteps[DefaultStep] = defaultValues;
        }
        
        public string Name;
        public ParamMeta.ParameterType Type;
        public (string, float) DefaultStep;
        public Dictionary<(string stepName, float stepValue) /*-1,0,1*/, List<MRBlendshapeSaveState>/*One for every renderer*/> AnimationSteps;

        public bool IsAssigned() => AnimationSteps.All(kvp => kvp.Value.Count != 0);
        public List<MRBlendshapeSaveState> GetDefaultValues() => AnimationSteps[DefaultStep];
    }
}