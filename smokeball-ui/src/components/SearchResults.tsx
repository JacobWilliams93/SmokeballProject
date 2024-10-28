import { SearchType } from "../enums/searchtype";
import { SearchResultModel } from "../models/searchResultModel";

interface SearchResultsProps{
    results: SearchResultModel
}

function SearchResults({results}: SearchResultsProps) {
    return (
        <>
            <h3 className="text-[24px]">The URL "{results.url}" appears in search engine {SearchType[results.searchType]} with search terms "{results.keywords}" at the following positions:</h3>
            <ul className="mt-[25px]">
                {results.searchPositions.map(r => (
                    <li>{r}</li>
                ))}
            </ul>
        </>
    );
}

export default SearchResults;