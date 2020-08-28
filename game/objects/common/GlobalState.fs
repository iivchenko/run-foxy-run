namespace RunFoxyRun

[<Sealed>]
type GlobalState private () =

    static member val State = GlobalState() with get

    member val Scores = 0 with get, set