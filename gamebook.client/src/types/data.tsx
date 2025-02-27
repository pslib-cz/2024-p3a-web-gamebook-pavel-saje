export type Buy = {
    buyID: number,
    interactibleID: number,
    itemID: number,
    interactible: Interactible,
    item: Item
}

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
    endID: number;
    end: End[];
    travelCost: number;
};

export type LocationContent = {
    locationContentID: number;
    locationID: number;
    interactibleID: number;
    xPos: number;
    yPos: number;
    size: number;
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

export type NpcContent = {
    npc: Npc;
    content: LocationContent;
}

export type RequiredItems = {
    requiredItemsID: number;
    locationID: number;
    itemID: number;
};

export type Sell = {
    sellID: number,
    interactibleID: number,
    itemID: number,
    interactible: Interactible,
    item: Item
}

export type Shops = {
    trades: Trades[],
    buys: Buy[],
    tradeInteractibles: TradesInteractible[],
    sells: Sell[]
}

export type Trade = {
    tradeID: number,
    item1: Item,
    item2: Item
}

export type Trades = {
    tradesID: number,
    interactibleID: number,
    interactible: Interactible,
    tradeID: number,
    trade: Trade
}

export type TradeInteractible = {
    tradeInteractibleID: number,
    interactibleID: number,
    itemID: number,
    interactible: Interactible,
    item: Item,
}

export type TradesInteractible = {
    tradesInteractibleID: number,
    tradeInteractibleID: number,
    interactibleID: number,
    tradeInteractible: TradeInteractible,
    interactible: Interactible,
    text: string,
    item: Item
}

export type Weapon = {
    weaponID: number;
    name: string;
    damage: number;
    itemID: number;
    item: Item;
};

export type End = {
    endID: number;
    LocatioID: number;
    DialogID: number;
};
