export interface InputItemCategory {
    name: string;
}

export interface InputInteractible {
    name: string;
    imagePath: string;
}

export interface InputLocationContent {
    locationID: number;
    interactibleID: number;
    xPos: number;
    yPos: number;
    size: number;
}

export interface InputLocation {
    name: string;
    imagePath: string;
    radiationGain: number;
}

