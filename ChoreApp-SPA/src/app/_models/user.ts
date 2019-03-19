import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    knownAs: string;
    age: number;
    gender: string;
    city: string;
    country: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    interests?: string;
    introduction?: string;
    photos?: Photo[];
    lookingFor?: string;
}
