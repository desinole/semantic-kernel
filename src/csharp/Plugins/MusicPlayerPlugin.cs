using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace Plugins;

public class MusicPlayerPlugin
{
    public bool IsPlaying { get; set; } = false;
    public int Volume { get; set; } = 50;

    [KernelFunction]
    [Description("Gets the state of the music player.")]
    public string GetState() => this.IsPlaying ? "playing" : "stopped";

    [KernelFunction]
    [Description("Changes the state of the music player.")]
    public string ChangeState(bool newState)
    {
        this.IsPlaying = newState;
        var state = this.GetState();

        // Print the state to the console
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"[Music player is now {state}]");
        Console.ResetColor();

        return state;
    }

    [KernelFunction]
    [Description("Gets the volume of the music player.")]
    public int GetVolume() => this.Volume;

    [KernelFunction]
    [Description("Changes the volume of the music player.")]
    public int ChangeVolume(int newVolume)
    {
        this.Volume = newVolume;

        // Print the volume to the console
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"[Volume is now {this.Volume}]");
        Console.ResetColor();

        return this.Volume;
    }
}
