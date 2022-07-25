public class PlayerObjectEvents
{
    public const string OnCollisionEnter2D = "OnCollisionEnter2D"; //Param: collision2D
    public const string OnCollisionExit2D = "OnCollisionExit2D"; //Param: collision2D

    public const string OnJumpStart = "OnPlayerJumpStart"; //Param: world position
    public const string OnJumpEnd = "OnPlayerJumpEnd"; //Param: world position

    public const string BeforePlayerDeath = "BeforePlayerDeath"; //No data
    public const string OnPlayerDeath = "OnPlayerDeath"; //GameObject of player
}