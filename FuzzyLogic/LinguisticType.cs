﻿using FuzzyLogic.Terms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyLogic
{
    public class LinguisticType
    {
        private IDictionary<string, ITerm> _terms;

        public LinguisticType(string name, double minValue = 0, double maxValue = int.MaxValue)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;

            _terms = new Dictionary<string, ITerm>();
        }

        public string Name { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public ITerm this[string index] => _terms[index];

        public IList<ITerm> Terms => _terms.Values.ToList();

        public LinguisticVariable CreateVariable(string name) => new LinguisticVariable(name, this);

        public void CreateTerm(string name, double center, double width, TermType termType = TermType.Middle)
        {
            _terms.Add(name, CreateTermInternal(name, center, width, termType));
        }

        private ITerm CreateTermInternal(string name, double center, double width, TermType termType)
        {
            switch (termType)
            {
                case TermType.Left: return new LeftSigmoidTerm(this, name, center, width);
                case TermType.Right: return new RightSigmoidTerm(this, name, center, width);
                case TermType.Middle: return new GaussTerm(this, name, center, width);
                default: throw new ApplicationException("Undefined term type.");
            }
        }

        public ILingusticVariableValue GetValue(double x) => new FixedValue(this, x);

        public ILingusticVariableValue GetValue(string name, double degree) => new FuzzyValue(this, this[name], degree);
    }
}
