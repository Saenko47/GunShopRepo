
const overlay = document.getElementById("overlay");

export function ToggleGlassEffect()
{
    overlay.classList.toggle("glass");

}

export function EnableGlassEffect() {
    if(overlay.classList.contains("glass")) return;
    overlay.classList.add("glass");
}

export function DisableGlassEffect() {
if(!overlay.classList.contains("glass")) return;
    overlay.classList.remove("glass");
}
