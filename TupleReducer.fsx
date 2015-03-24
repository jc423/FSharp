

module TupleOps =  
    let rec createPairs (list) =
        match list with
        |[]->[]
        | head::tail -> let h = head
                        match tail with
                        |[]->[]
                        | head::tail -> (h,head)::createPairs tail

module Team = 
    type Team = { Name: string;
        Ranking: int;
        Seed: int; }
        
    //teams in regions
    let midWestRegionTeams = [{Name = "Kentucky"; Ranking = 1;Seed = 1 };{Name = "Hampton"; Ranking = 70;Seed = 16};
                              {Name = "Cincinatti"; Ranking = 30; Seed = 8};{Name = "Purdue"; Ranking = 31; Seed = 9};
                              {Name = "West Virgina"; Ranking = 12; Seed = 5};{Name = "UB"; Ranking = 37; Seed = 12};
                              {Name = "Maryland"; Ranking = 22; Seed = 4};{Name = "Valpraiso"; Ranking = 37; Seed = 16};
                              {Name = "Butler"; Ranking = 14; Seed = 6};{Name = "Texas"; Ranking = 29; Seed = 11};
                              {Name = "Notre Dame"; Ranking = 6; Seed = 3};{Name = "Northeastern"; Ranking = 71; Seed = 14};
                              {Name = "Wichita State"; Ranking = 22; Seed = 7};{Name = "Indiana"; Ranking = 31; Seed = 10};
                              {Name = "Kansas"; Ranking = 4; Seed = 2};{Name = "New Mexico St"; Ranking = 47; Seed = 15}]

    let westRegionTeams = [{Name = "Wisconsin"; Ranking = 1;Seed = 1 };{Name = "Coastal Carolina"; Ranking = 69;Seed = 16};
                              {Name = "Oregon"; Ranking = 30; Seed = 8};{Name = "OK St"; Ranking = 30; Seed = 9};
                              {Name = "Arkansas"; Ranking = 12; Seed = 5};{Name = "Wofford"; Ranking = 33; Seed = 12};
                              {Name = "North Carolina"; Ranking = 14; Seed = 4};{Name = "Harvard"; Ranking = 29; Seed = 13};
                              {Name = "Xavier"; Ranking = 9; Seed = 6};{Name = "Ole Miss"; Ranking = 39; Seed = 11};
                              {Name = "Baylor"; Ranking = 5; Seed = 3};{Name = "Georgia State"; Ranking = 51; Seed = 14};
                              {Name = "VCU"; Ranking = 27; Seed = 7};{Name = "Ohio St"; Ranking = 34; Seed = 10};
                              {Name = "Arizona"; Ranking = 2; Seed = 2};{Name = "Texas Southern"; Ranking = 47; Seed = 15}]

    let eastRegionTeams = [{Name = "Villanova"; Ranking =3;Seed = 1 };{Name = "Lafayette"; Ranking = 68;Seed = 16};
                              {Name = "NC State"; Ranking = 27; Seed = 8};{Name = "LSU"; Ranking = 30; Seed = 9};
                              {Name = "Northern Iowa"; Ranking = 12; Seed = 5};{Name = "Wyoming"; Ranking = 37; Seed = 12};
                              {Name = "Louisville"; Ranking = 7; Seed = 4};{Name = "UC Irvine"; Ranking = 32; Seed = 16};
                              {Name = "Providence"; Ranking = 26; Seed = 6};{Name = "Dayton"; Ranking = 39; Seed = 11};
                              {Name = "Oklahoma"; Ranking = 11; Seed = 3};{Name = "Albany"; Ranking = 41; Seed = 14};
                              {Name = "Michigan State"; Ranking = 28; Seed = 7};{Name = "Georgia"; Ranking = 61; Seed = 10};
                              {Name = "Virginia"; Ranking = 2; Seed = 2};{Name = "Belmont"; Ranking = 57; Seed = 15}]

    let southRegionTeams = [{Name = "Duke"; Ranking = 3;Seed = 1 };{Name = "Robert Morris"; Ranking = 49;Seed = 16};
                              {Name = "San Diego St"; Ranking = 34; Seed = 8};{Name = "St Johns"; Ranking = 37; Seed = 9};
                              {Name = "Utah"; Ranking = 19; Seed = 5};{Name = "SF Austin"; Ranking = 42; Seed = 12};
                              {Name = "Georgetown"; Ranking = 40; Seed = 4};{Name = "Eastern Washington"; Ranking = 63; Seed = 13};
                              {Name = "SMU"; Ranking = 13; Seed = 6};{Name = "UCLA"; Ranking = 53; Seed = 11};
                              {Name = "Iowa St"; Ranking = 8; Seed = 3};{Name = "UAB"; Ranking = 54; Seed = 14};
                              {Name = "Iowa"; Ranking = 18; Seed = 7};{Name = "Davidson"; Ranking = 60; Seed = 10};
                              {Name = "Gonzaga"; Ranking = 7; Seed = 2};{Name = "North Dakota St"; Ranking = 67; Seed = 15}]

    let getOddsByRankDiff (difference) =
        if difference < 1 then 0.5
        elif difference < 4 then 0.55
        elif difference < 8 then 0.6
        elif difference < 12 then 0.65
        elif difference < 16 then 0.7
        elif difference < 20 then 0.75
        elif difference < 24 then 0.80
        else 0.95       

module BracketPicker = 
    open System
    let rnd = new Random()
    //determine winner with random number generator, better ranked team has better odds
    let calcWinner ((a : Team.Team),(b : Team.Team)) =  let rankedOrder = if(a.Ranking <= b.Ranking) then (a,b)
                                                                          else (b,a)
                                                        if (rnd.NextDouble() < Team.getOddsByRankDiff(abs(a.Ranking - b.Ranking))) then fst(rankedOrder)
                                                        else snd(rankedOrder)

    //make regional brackets TODO - take raw teams and order by seeds
    let mwRegionBracket = TupleOps.createPairs Team.midWestRegionTeams
    let wRegionBracket = TupleOps.createPairs Team.westRegionTeams
    let eastRegionBracket = TupleOps.createPairs Team.eastRegionTeams
    let southRegionBracket = TupleOps.createPairs Team.southRegionTeams

    let rec bracketPicker (list: (Team.Team*Team.Team) list) =
        match list with
        | [] -> []
        | [a] -> [a]
        | _ -> Console.WriteLine(sprintf "%A" list);bracketPicker (TupleOps.createPairs (List.map(calcWinner) list))

    //evaluate to championship game
    let champion = calcWinner ((bracketPicker (bracketPicker (mwRegionBracket @ wRegionBracket)) @ (bracketPicker (eastRegionBracket @ southRegionBracket))).Item(0))

