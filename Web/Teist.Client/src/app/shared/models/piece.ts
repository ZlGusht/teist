import { BaseEntity } from './contracts/base-entity.interface';

export class Piece implements BaseEntity {
    id: number;
    name: string;
    genre: string;
    pieceType: string;
    performer: string;
    collaborators: string[];

    constructor(piece: any, performer, collabs) {
        this.name = piece.name;
        this.genre = piece.genre;
        this.pieceType = piece.pieceType;
        this.performer = performer;
        this.collaborators = collabs;

    }
}