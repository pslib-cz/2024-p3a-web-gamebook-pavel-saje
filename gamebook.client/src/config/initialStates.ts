export const initialStateMap: Record<string, Record<string, unknown>> = {
    'Location': {
        name: '',
        imagePath: '',
        radiationGain: 0
    },
    'Interactible': {
        name: '',
        imagePath: ''
    },
    'LocationContent': {
        locationID: 0,
        interactibleID: 0,
        xPos: 0,
        yPos: 0,
        size: 0
    },
    'ItemCategory': {
        name: ''
    },
    'InteractiblesItem':{
        interactibleId: 0,
        itemId:0
    },
    'DataInteractiblesOption':{
        interactibleId:0,
        optionId:0
    },
    'Dialog':{
        fromInteractibleID: 0,
        iteractibleID: 0,
        nextDialogID: 0,
        text: ''
    },
    'DialogResponse':{
        dialogID: 0,
        nextDialogID: 0,
        responseText: '',
        relationshipEffect: 0
    },
    'Item':{
        name:'',
        tradeValue:0,
        stackable:false,
        radiationGain:0,
        categoryId:0
    },
    'LocationPath':{
        firstNodeID: 0,
        secondNodeID:0,
        energyTravelCost:0
    }
};
