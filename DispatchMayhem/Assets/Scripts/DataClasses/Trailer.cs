using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer
{
    int space;
    int spaceLiquid;

    enum TrailerType
    {
        FLATBED,
        DROPDECK,
        DRYVAN,
        REFERVAN
    }

    class Flatbed : Trailer
    {
        TrailerType status = TrailerType.FLATBED;
    }

    class DropDeck : Trailer
    {
        TrailerType status = TrailerType.DROPDECK;
    }

    class DryVan : Trailer
    {
        TrailerType status = TrailerType.DRYVAN;
    }

    class ReferVan : Trailer
    {
        TrailerType status = TrailerType.REFERVAN;
    }

    public void AllowedLoads()
    {
        
    }
}
