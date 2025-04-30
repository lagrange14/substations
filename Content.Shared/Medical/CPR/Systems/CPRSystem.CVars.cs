using Content.Shared._EE.CCVars;
using Robust.Shared.Configuration;

namespace Content.Shared.Medical.CPR
{
    public sealed partial class CPRSystem
    {
        [Dependency] private readonly IConfigurationManager _cfg = default!;

        public bool EnableCPR { get; private set; }
        public bool HealsAirloss { get; private set; }
        public bool ReducesRot { get; private set; }
        public float ResuscitationChance { get; private set; }
        public float RotReductionMultiplier { get; private set; }
        public float AirlossReductionMultiplier { get; private set; }

        private void InitializeCVars()
        {
            Subs.CVar(_cfg, CPRCCVars.EnableCPR, value => EnableCPR = value, true);
            Subs.CVar(_cfg, CPRCCVars.CPRHealsAirloss, value => HealsAirloss = value, true);
            Subs.CVar(_cfg, CPRCCVars.CPRReducesRot, value => ReducesRot = value, true);
            Subs.CVar(_cfg, CPRCCVars.CPRResuscitationChance, value => ResuscitationChance = value, true);
            Subs.CVar(_cfg, CPRCCVars.CPRRotReductionMultiplier, value => RotReductionMultiplier = value, true);
            Subs.CVar(_cfg, CPRCCVars.CPRAirlossReductionMultiplier, value => AirlossReductionMultiplier = value, true);
        }
    }
}
