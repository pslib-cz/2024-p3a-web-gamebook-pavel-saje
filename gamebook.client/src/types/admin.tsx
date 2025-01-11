export type TableField = {
    key: string;
    label: string;
    type: "string" | "number" | "boolean" | "image";
};

export type EndpointHeader = {
    primaryKey: string;
    content: TableField[];
};

//headers

export const EndpointHeaderMap: Record<string, EndpointHeader> = {
    "/api/Category": {
        primaryKey: "categoryID",
        content: [
            {
                key: "name",
                label: "Category Name",
                type: "string",
            },
        ],
    },
    "/api/Consumable": {
        primaryKey: "consumableItemID",
        content: [
            {
                key: "itemID",
                label: "Item ID",
                type: "number",
            },
            {
                key: "healthValue",
                label: "Health Value",
                type: "number",
            },
            {
                key: "energyValue",
                label: "Energy Value",
                type: "number",
            },
            {
                key: "radiationValue",
                label: "Radiation Value",
                type: "number",
            },
        ],
    },
    "/api/InteractibleItems": {
        primaryKey: "interactibleItemID",
        content: [
            {
                key: "interactibleID",
                label: "Interactible ID",
                type: "number",
            },
            {
                key: "itemID",
                label: "Item ID",
                type: "number",
            },
        ],
    },
    "/api/InteractibleOptions": {
        primaryKey: "interactibleOptionID",
        content: [
            {
                key: "interactibleID",
                label: "Interactible ID",
                type: "number",
            },
            {
                key: "optionID",
                label: "Option ID",
                type: "number",
            },
        ],
    },
    "/api/Interactibles": {
        primaryKey: "interactibleID",
        content: [
            {
                key: "name",
                label: "Name",
                type: "string",
            },
            {
                key: "image",
                label: "Image",
                type: "image",
            },
        ],
    },
    "/api/Items": {
        primaryKey: "itemID",
        content: [
            {
                key: "name",
                label: "Item Name",
                type: "string",
            },
            {
                key: "tradeValue",
                label: "Trade Value",
                type: "number",
            },
            {
                key: "stackable",
                label: "Stackable",
                type: "boolean",
            },
            {
                key: "categoryId",
                label: "Category ID",
                type: "number",
            },
        ],
    },
    "/api/LocationContent": {
        primaryKey: "locationContentID",
        content: [
            {
                key: "locationID",
                label: "Location ID",
                type: "number",
            },
            {
                key: "interactibleID",
                label: "Interactible ID",
                type: "number",
            },
            {
                key: "xPos",
                label: "X Position",
                type: "number",
            },
            {
                key: "yPos",
                label: "Y Position",
                type: "number",
            },
        ],
    },
    "/api/LocationPaths": {
        primaryKey: "pathID",
        content: [
            {
                key: "firstNodeID",
                label: "1. Node ID",
                type: "number",
            },
            {
                key: "secondNodeID",
                label: "2. Node ID",
                type: "number",
            },
        ],
    },
    "/api/Locations": {
        primaryKey: "locationID",
        content: [
            {
                key: "backgroundImage",
                label: "Image",
                type: "image",
            },
            {
                key: "name",
                label: "Location Name",
                type: "string",
            },
            {
                key: "radiationGain",
                label: "Radiation Gain",
                type: "number",
            },
        ],
    },
    "/api/NpcDialog": {
        primaryKey: "dialogID",
        content: [
            {
                key: "iteractibleID",
                label: "Interactible ID",
                type: "number",
            },
            {
                key: "dialogOrder",
                label: "Dialog Order",
                type: "number",
            },
            {
                key: "text",
                label: "Dialog Text",
                type: "string",
            },
        ],
    },
    "/api/NpcDialogResponses": {
        primaryKey: "dialogResponseID",
        content: [
            {
                key: "dialogID",
                label: "Dialog ID",
                type: "number",
            },
            {
                key: "responseText",
                label: "Response",
                type: "string",
            },
            {
                key: "relationshipEffect",
                label: "Relationship Effect",
                type: "number",
            },
        ],
    },
    "/api/OptionsEnum": {
        primaryKey: "optionID",
        content: [
            {
                key: "optionText",
                label: "Option Text",
                type: "string",
            },
        ],
    },
};
