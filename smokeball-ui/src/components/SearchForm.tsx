import { useState } from "react";
import { SearchType } from "../enums/searchtype";
import SearchResults from "./SearchResults";
import { SearchResultModel } from "../models/searchResultModel";

function SearchForm(){
    const [keywords, setKeywords] =  useState('');
    const [url, setUrl] = useState('');
    const [searchType, setSearchType] = useState(SearchType.Google);
    const [result, setResult] = useState<SearchResultModel | undefined>(undefined);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
     
    const search = async () => {
        if(!keywords || !url){
            setError("Keywords and Url must both contain values");
            return;
        }
        setError('');
        try{
            setIsLoading(true);
            const response = await fetch(`http://localhost:5219/search?searchString=${keywords}&targetUrl=${url}&engineType=${searchType}`);
            const data : SearchResultModel = await response.json();
            setResult(data);
            setIsLoading(false);
        } catch (error){
            console.log(error);
        }
    }

    return (
    <>
        <img className="mb-[50px]" src="https://cdn.prod.website-files.com/5f9c3b963e528f64676e0da1/65fa5ac905862b6a7297c037_logo_full_horizontal-OrangeBlack-p-500.png"/>

        <div className="page">
            <div className="form">

                <div className="field">
                    <label>Keywords:</label>
                    <input name="keywords" value={keywords} onChange={e => setKeywords(e.target.value)}/>
                </div>
                <div className="field"> 
                    <label>Url: </label>
                    <input name="url" value={url} onChange={e => setUrl(e.target.value)}/>
                </div>
                <div className="field">
                    <label>Search Engine</label>
                    <select value={searchType} onChange={e => {
                        setSearchType(e.target.value as unknown as SearchType);
                    }}>
                        {
                            Object.values(SearchType).filter(val => typeof val === "string").map( (val, index) => (
                                <option value={index} >
                                    {val}
                                </option>
                            ))
                        }
                    </select>
                </div>
                <button className="mt-[24px]" type="submit" onClick={search} disabled={isLoading}>{isLoading ? "Loading" : "Search"}</button>
            </div>
            <div className="result">
                {result && !error && !isLoading &&(
                    <SearchResults
                        results={result}
                    />
                )}
                {error && (
                    <div className="text-[red]">
                        <p>{error}</p>
                    </div>
                )}
            </div>
        </div>
    </>
    );
}

export default SearchForm;