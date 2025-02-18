export type ConsumableItem = {
    consumableItemID: number;
    itemID: number;
    healthValue: number;
    energyValue: number;
    radiationValue: number;
    item: Item;
};

export type Dialog = {
    dialogID: number;
    iteractibleID: number;
    nextDialogID: number;
    text: string;
    interactible: Interactible;
    dialogResponses: DialogResponse[];
};

export type DialogResponse = {
    dialogResponseID: number;
    dialogID: number;
    responseText: string;
    relationshipEffect: number;
    dialog: Dialog;
    nextDialogID: number;
};

export type Interactible = {
    interactibleID: number;
    name: string;
    imagePath: string;
    imageBase64: string;
};

export type InteractiblesItem = {
    interactibleItemID: number;
    interactibleID: number;
    fromInteractibleID: number;
    itemId: number;
    interactible: Interactible;
    fromInteractible: Interactible;
    item: Item;
};

export type InteractiblesOption = {
    interactibleOptionID: number;
    interactibleID: number;
    optionID: number;
    interactible: Interactible;
    option: InteractOption;
};

export type InteractOption = {
    optionID: number;
    optionText: string;
};

export type Item = {
    itemID: number;
    name: string;
    tradeValue: number;
    stackable: boolean;
    categoryId: number;
    category: ItemCategory;
    radiationGain: number;
};

export type ItemTypes = {
    consumables: ConsumableItem[];
    weapons: Weapon[];
};

export type ItemCategory = {
    categoryID: number;
    name: string;
};

export type DataLocation = {
    locationID: number;
    name: string;
    backgroundImagePath: string;
    radiationGain: number;
    requiredItems: RequiredItems[];
    locationContents: LocationContent[];
    backgroundImageBase64: string;
};

export type LocationContent = {
    locationContentID: number;
    locationID: number;
    interactibleID: number;
    xPos: number;
    yPos: number;
    location: Location;
    interactible: Interactible
};

export type LocationPath = {
    pathID: number;
    firstNodeID: number;
    secondNodeID: number;
    energyTravelCost: number;
    firstNode: Location;
    secondNode: Location;
};

export type Npc = {
    npcID: number;
    name: string;
    health: number;
    weapon: Weapon;
}

export type RequiredItems = {
    requiredItemsID: number;
    locationID: number;
    itemID: number;
};

export type Weapon = {
    weaponID: number;
    name: string;
    damage: number;
    itemID: number;
    item: Item;
};

export type End = {
    endID: number;
    text: string;
    imagePath: string;
};
