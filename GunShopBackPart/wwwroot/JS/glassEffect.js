
const overlay = document.getElementById("overlay");

export function ToggleGlassEffect()
{
    overlay.classList.toggle("glass");

}

export function EnableGlassEffect() {
    overlay.classList.add("glass");
}

export function DisableGlassEffect() {
    overlay.classList.remove("glass");
}
