using Robust.Shared.Configuration;
// ReSharper disable InconsistentNaming

namespace Content.Shared._EE.CCVars;

[CVarDefs]
public sealed class CPRCCVars
{

        /// <summary>
        ///     Controls whether the entire CPR system runs. When false, nobody can perform CPR. You should probably remove the trait too
        ///     if you are wishing to permanently disable the system on your server.
        /// </summary>
        public static readonly CVarDef<bool> EnableCPR =
            CVarDef.Create("cpr.enable", true, CVar.REPLICATED | CVar.SERVER);

        /// <summary>
        ///     By default, CPR reduces rot timers by an amount of seconds equal to the time spent performing CPR as an abstraction of delaying brain death. This is an optional multiplier that can increase or decrease the amount
        ///     of rot reduction. Set it to 2 for if you want 3 seconds of CPR to reduce 6 seconds of rot.
        /// </summary>
        /// <remarks>
        ///     If you're wondering why there isn't a CVar for setting the duration of the doafter, that's because it's not actually possible to have a timespan in cvar form
        ///     Curiously, it's also not possible for **shared** systems to set variable timespans. Which is where this system lives.
        /// </remarks>
        public static readonly CVarDef<float> CPRRotReductionMultiplier =
            CVarDef.Create("cpr.rot_reduction_multiplier", 1f, CVar.REPLICATED | CVar.SERVER);

        /// <summary>
        ///     By default, CPR heals airloss by 1 point for every second spent performing CPR, meaning that airloss damage stays steady as opposed to accumulting. Just like above, this directly multiplies the healing amount, meaning that this would *heal* airloss damage.
        ///     Set it to -2 to get 6 points of airloss healing for every 3 seconds of CPR.
        /// </summary>
        public static readonly CVarDef<float> CPRAirlossReductionMultiplier =
            CVarDef.Create("cpr.airloss_reduction_multiplier", -0.5f, CVar.REPLICATED | CVar.SERVER);
}
