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
