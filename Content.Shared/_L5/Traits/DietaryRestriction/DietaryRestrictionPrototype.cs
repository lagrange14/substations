using Content.Shared.Whitelist;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._L5.Traits.DietaryRestriction;

/// <summary>
/// Prototype representing a dietary restriction. The DietaryRestrictionComponent may hold several of these.
/// </summary>
[Prototype]
public sealed partial class DietaryRestrictionPrototype : IPrototype
{
    [IdDataField, ViewVariables]
    public string ID { get; private set; } = string.Empty;

    /// <summary>
    /// Processed first, exceptions stop processing other rules.
    /// E.g: Pescetarians can eat fish, but fish dishes are tagged Meat and Fish. Exception for Fish means Meat will not fail. Just have to hope surf-and-turf isn't a thing...
    /// </summary>
    [DataField]
    public EntityWhitelist? Exceptions = null;

    /// <summary>
    /// Foods in this list are desired. Eating something not in this list negatively impacts the entity's mental health via popup.
    /// </summary>
    [DataField]
    public EntityWhitelist? Wants = null;

    /// <summary>
    /// Foods in this list are not wanted. Eating something in this list negatively impacts the entity's mental health via popup.
    /// </summary>
    [DataField]
    public EntityWhitelist? DoesNotWant = null;

    /// <summary>
    /// Only foods in this list can be eaten.
    /// </summary>
    [DataField]
    public EntityWhitelist? MustHave = null;

    /// <summary>
    /// Foods in this list cannot be eaten.
    /// </summary>
    [DataField]
    public EntityWhitelist? MustNotHave = null;

    /// <summary>
    /// Foods in this list can be eaten, but will cause an allergic reaction.
    /// </summary>
    [DataField]
    public EntityWhitelist? AllergicTo = null;

    /// <summary>
    /// The chance of vomiting if a user eats something they don't want
    /// It's an oversimplification, but the more "important" a restriction is to a person,
    /// the greater the chance they'll be made ill by eating it. This will probably be
    /// replaced by the mood system when added.
    /// </summary>
    [DataField]
    public float ChanceOfVomiting = 0.05f;

    /// <summary>
    /// The amount of histamine to add  when an allergen is ingested. Three bites should send you to crit.
    /// </summary>
    [DataField]
    public float HistamineAmount = 5f;
}
