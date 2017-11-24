using Godot;

public class HUD : CanvasLayer
{

    private Control main;
    private Control interaction;
    private Control menu;

    private float maxHealth = 100f;
    private float maxStamina = 10f;

    public override void _Ready()
    {
        main = (Control)GetNode("Main");
        interaction = (Control)GetNode("Interaction");
        menu = (Control)GetNode("Menu");
        ChangeDirection(0);
    }

    public void ChangeDirection(float degrees)
    {
        Container compassContainer = (Container)main.GetNode("VBoxContainer/Top/CompassContainer");
        TextureRect compass = (TextureRect)main.GetNode("VBoxContainer/Top/CompassContainer/Compass");

        float northPos = (compassContainer.RectSize.x - compass.RectSize.x) / 2;
        float offset = (degrees % 360) * (compass.RectSize.x / 900); //Make sure degrees is between -360,360. 900 degrees are present in the texture
        
        compass.RectPosition = new Vector2(northPos + offset, compass.RectPosition.y);
    }

    public void SetMaxHealth(float health)
    {
        this.maxHealth = health;
    }

    public void SetMaxStamina(float stamina)
    {
        this.maxStamina = stamina;
    }

    public void SetHealth(float health)
    {
        Control healthBar = ((Control)main.GetNode("VBoxContainer/Bottom/Centre/Health"));
        healthBar.AnchorRight = health / maxHealth;
        healthBar.MarginRight = 0;
    }

    public void SetStamina(float stamina)
    {
        Control staminaBar = ((Control)main.GetNode("VBoxContainer/Bottom/Centre/Stamina"));
        staminaBar.AnchorRight = stamina / maxStamina;
        staminaBar.MarginRight = 0;
    }
}
