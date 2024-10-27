import { SearchType } from "../enums/searchtype";

export interface SearchResultModel{
    keywords: string,
    url: string,
    searchType: SearchType,
    searchPositions: number[]
}