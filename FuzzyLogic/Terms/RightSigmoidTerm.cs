﻿using System;

namespace FuzzyLogic.Terms
{
    class RightSigmoidTerm : ITerm
    {
        public RightSigmoidTerm(LinguisticType type, string name, double center, double width)
        {
            Type = type;
            Name = name;
            Center = center;
            Width = width;
        }

        public LinguisticType Type { get; private set; }

        public string Name { get; private set; }

        public double Center { get; private set; }

        public double Width { get; private set; }

        public double WeightCenter => Math.Min(Type.MaxValue, Center + Width);

        public double CalcTruthDegree(double x) => 1.0 - 1.0 / (1.0 + Math.Exp((x - Center) / Width));
    }
}

// s = 1 -  1/(1+exp((x-center)/width)) 

