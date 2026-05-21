
import { collectFilter } from "./dataCollector.js";
import { sendToServer } from "./sendToServer.js";
 export function leftButtonHandler(state,productsContainer,filterType,productsPerPageSelector  )
{
    if (state.pageCounter > 1)
    {
        productsContainer.innerHTML = "";

        state.pageCounter--;

        document.getElementById("currentPage").textContent =
            state.pageCounter;

        const filter = collectFilter(filterType);
        const type = filterType.dataset.type;
        const productsPerPage =
            parseInt(productsPerPageSelector.value);

        sendToServer(
            state.pageCounter,
            productsPerPage,
            filter,
            type
        );
    }
}

 export function rightButtonHandler(state,productsContainer,filterType,productsPerPageSelector)
{
    if (state.pageCounter < state.maxOfPages)
    {
        productsContainer.innerHTML = "";

        state.pageCounter++;

        document.getElementById("currentPage").textContent =
            state.pageCounter;

        const filter = collectFilter(filterType);
        const type = filterType.dataset.type;
        const productsPerPage =
            parseInt(productsPerPageSelector.value);

        sendToServer(
            state.pageCounter,
            productsPerPage,
            filter,
            type
        );
    }
}

export function HandleFilterButton(container)
{
    if(!container.classList.contains("hidden")) container.classList.add("hidden");
    else container.classList.remove("hidden");
      
}