export function CreateInput(type, name, value = "", step = null)
{
    const input = document.createElement("input");

    input.type = type;
    input.name = name;
    input.value = value ?? "";

    if(step)
    {
        input.step = step;
    }

    return input;
}

export function CreateHiddenInput(name, value)
{
    const input = document.createElement("input");

    input.type = "hidden";
    input.name = name;
    input.value = value;

    return input;
}

export function CreateTextarea(name, value = "")
{
    const textarea = document.createElement("textarea");

    textarea.name = name;
    textarea.value = value ?? "";

    return textarea;
}

export function CreateSelect(name, enumObject, selectedValue)
{
    const select = document.createElement("select");

    select.name = name;

    Object.entries(enumObject).forEach(([key, value]) => {

        const option = document.createElement("option");

        option.value = key;
        option.textContent = value;

        if(Number(key) === selectedValue)
        {
            option.selected = true;
        }

        select.appendChild(option);
    });

    return select;
}