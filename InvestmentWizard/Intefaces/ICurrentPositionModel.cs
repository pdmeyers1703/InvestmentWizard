namespace InvestmentWizard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICurrentPositionsModel
    {
        List<IOpenPositions> CurrentPositions { get; }

        void UpdateList();

        void Update();

        void BuildTotals();
    }
}
