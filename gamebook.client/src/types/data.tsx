export type ConsumableItem = {
    ConsumableItemID : number,
    ItemID : number,
    HealthValue : number,
    EnergyValue : number,
    RadiationValue : number,
    Item : Item,
}

export type Dialog = {
    DialogID : number,
    IteractibleID : number,
    DialogOrder : number,
    Text : string,
    Interactible : Interactible,
    DialogResponses : Array<DialogResponse>,
}

export type DialogResponse = {
    DialogResponseID : number,
    DialogID : number,
    ResponseText : string,
    RelationshipEffect : number,
    Dialog : Dialog
}

export type Interactible = {
    IteractibleID : number,
    Name : string,
    Image : string
}

export type InteractiblesItem = {
    InteractibleItemID : number,
    InteractibleID : number,
    ItemId : number,
    Interactible : Interactible,
    Item : Item
}

export type InteractiblesOption = {
    InteractibleOptionID : number,
    InteractibleID : number,
    OptionID : number,
    Interactible : Interactible,
    Option : InteractiblesOption
}

export type InteractOption = {
    OptionID : number,
    OptionText : string
}

export type Item = {
    ItemID : number,
    Name : string,
    TradeValue : number,
    Stackable : boolean,
    CategoryId : number,
    Category : ItemCategory
}

export type ItemCategory = {
    CategoryID : number,
    Name : string
}

export type Location = {
    LocationID : number,
    name : string,
    BackgroundImage : string,
    RadiationGain : number
}

export type LocationContent = {
    LocationContentID : number,
    LocationID : number,
    InteractibleID : number,
    XPos : number,
    YPos : number,
    Location : Location,
    Interactible : Interactible
}

export type LocationPath = {
    PathID : number,
    FirstNodeID : number,
    SecondNodeID : number,
    EnergyTravelCost : number,
    FirstNode : Location,
    SecondNode : Location
}