namespace Moss.ApiClient.SomaFm;

/// <summary>
/// Represents channel ID
/// </summary>
public record ChannelId
{
    /// <summary>Beat Blender</summary>
    public static readonly ChannelId BeatBlender = new("beatblender");

    /// <summary>Black Rock FM</summary>
    public static readonly ChannelId BlackRockFM = new("brfm");

    /// <summary>Boot Liquor</summary>
    public static readonly ChannelId BootLiquor = new("bootliquor");

    /// <summary>cliqhop idm</summary>
    public static readonly ChannelId cliqhopidm = new("cliqhop");

    /// <summary>Covers</summary>
    public static readonly ChannelId Covers = new("covers");

    /// <summary>Deep Space One</summary>
    public static readonly ChannelId DeepSpaceOne = new("deepspaceone");

    /// <summary>DEF CON Radio</summary>
    public static readonly ChannelId DEFCONRadio = new("defcon");

    /// <summary>Digitalis</summary>
    public static readonly ChannelId Digitalis = new("digitalis");

    /// <summary>Drone Zone</summary>
    public static readonly ChannelId DroneZone = new("dronezone");

    /// <summary>Dub Step Beyond</summary>
    public static readonly ChannelId DubStepBeyond = new("dubstep");

    /// <summary>Fluid</summary>
    public static readonly ChannelId Fluid = new("fluid");

    /// <summary>Folk Forward</summary>
    public static readonly ChannelId FolkForward = new("folkfwd");

    /// <summary>Groove Salad</summary>
    public static readonly ChannelId GrooveSalad = new("groovesalad");

    /// <summary>Groove Salad Classic</summary>
    public static readonly ChannelId GrooveSaladClassic = new("gsclassic");

    /// <summary>Heavyweight Reggae</summary>
    public static readonly ChannelId HeavyweightReggae = new("reggae");

    /// <summary>Illinois Street Lounge</summary>
    public static readonly ChannelId IllinoisStreetLounge = new("illstreet");

    /// <summary>Indie Pop Rocks!</summary>
    public static readonly ChannelId IndiePopRocks = new("indiepop");

    /// <summary>Left Coast 70s</summary>
    public static readonly ChannelId LeftCoast70s = new("seventies");

    /// <summary>Lush</summary>
    public static readonly ChannelId Lush = new("lush");

    /// <summary>Metal Detector</summary>
    public static readonly ChannelId MetalDetector = new("metal");

    /// <summary>Mission Control</summary>
    public static readonly ChannelId MissionControl = new("missioncontrol");

    /// <summary>n5MD Radio</summary>
    public static readonly ChannelId n5MDRadio = new("n5md");

    /// <summary>PopTron</summary>
    public static readonly ChannelId PopTron = new("poptron");

    /// <summary>Secret Agent</summary>
    public static readonly ChannelId SecretAgent = new("secretagent");

    /// <summary>Seven Inch Soul</summary>
    public static readonly ChannelId SevenInchSoul = new("7soul");

    /// <summary>SF 10-33</summary>
    public static readonly ChannelId SF1033 = new("sf1033");

    /// <summary>SF Police Scanner</summary>
    public static readonly ChannelId SFPoliceScanner = new("scanner");

    /// <summary>SomaFM Live</summary>
    public static readonly ChannelId SomaFMLive = new("live");

    /// <summary>SomaFM Specials</summary>
    public static readonly ChannelId SomaFMSpecials = new("specials");

    /// <summary>Sonic Universe</summary>
    public static readonly ChannelId SonicUniverse = new("sonicuniverse");

    /// <summary>Space Station Soma</summary>
    public static readonly ChannelId SpaceStationSoma = new("spacestation");

    /// <summary>Suburbs of Goa</summary>
    public static readonly ChannelId SuburbsofGoa = new("suburbsofgoa");

    /// <summary>Synphaera Radio</summary>
    public static readonly ChannelId SynphaeraRadio = new("synphaera");

    /// <summary>The Trip</summary>
    public static readonly ChannelId TheTrip = new("thetrip");

    /// <summary>ThistleRadio</summary>
    public static readonly ChannelId ThistleRadio = new("thistle");

    /// <summary>Underground 80s</summary>
    public static readonly ChannelId Underground80s = new("u80s");

    /// <summary>Vaporwaves</summary>
    public static readonly ChannelId Vaporwaves = new("vaporwaves");

    /// <summary>Value</summary>
    public string Value { get; }

    private ChannelId(string value)
    {
        Value = value;
    }
}
