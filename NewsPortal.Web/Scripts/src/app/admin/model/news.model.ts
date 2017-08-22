import { IAuthor } from './author.model';
export interface INewsItem {
    id: string;
    title: string;
    content: string;
    author: string;
    authorId: string;
}
