import{GetCountOfProductsThisFilter} from "./countOfProductsThisFilter.js";
import{sendToServer} from "./sendToServer.js";
import{GetForm} from "./filterFormSelector.js";
import{collectFilter} from "./dataCollector.js";
import{leftButtonHandler, rightButtonHandler, HandleFilterButton} from "./paginationButtons.js";

const innerFilter = document.getElementById("filterForm");
const select = document.getElementById("filterProductTypeSelector");
const productsPerPageSelector = document.getElementById("productsPerPageSelectorId");
const productsContainer = document.getElementById("productContainerId");

const leftButton = document.getElementById("leftButton");
const rightButton = document.getElementById("rightButton");

const serchButton = document.getElementById("searchButtonId");
const searchInput = document.getElementById("searchInputId");

const filterButton = document.getElementById("showFilter");
const filterContainer = document.getElementById("filterContainerId");

const btn = document.getElementById("UserInfoId");

export const paginationState = 
{
    pageCounter: 1,
    maxOfPages: 0
};

const totalProducts = 0;





select.addEventListener("change", () => 
    {
        const result = GetForm(select.value);

    if (!result) {
        innerFilter.innerHTML = "";
        return;
    }
    innerFilter.dataset.type = result.type;

    innerFilter.innerHTML = `
      
            ${result.html}
      
    `;
        if(select.value != "-")
            {
                innerFilter.innerHTML += ' <br><button type = "submit" class = "searchButton" >Enter</button>'
            }
    })


    function renderPage()
    {
         document.getElementById("pagination").classList.remove("hidden");

    paginationState.pageCounter = 1;
    const form = e.target;
    const filter = collectFilter(form);

    console.log(filter);
    let type = form.dataset.type;

    console.log("Type:", type);
    console.log("Filter:", filter);
     const productsPerPage = parseInt(productsPerPageSelector.value);

    GetCountOfProductsThisFilter(filter, select.value, productsPerPage)
    .then(count => {
        paginationState.maxOfPages = count;

        document.getElementById("totalProducts").textContent = paginationState.maxOfPages;
        document.getElementById("currentPage").textContent = paginationState.pageCounter;
    });
    sendToServer(paginationState.pageCounter, productsPerPage, filter, type, name);
    }

    document.addEventListener("submit", function (e) {
    if (!e.target.matches("form")) return;
    productsContainer.innerHTML = "";
    e.preventDefault();

    document.getElementById("pagination").classList.remove("hidden");

    paginationState.pageCounter = 1;
    const form = e.target;
    const filter = collectFilter(form);

    console.log(filter);
    let type = form.dataset.type;

    console.log("Type:", type);
    console.log("Filter:", filter);
     const productsPerPage = parseInt(productsPerPageSelector.value);

    GetCountOfProductsThisFilter(filter, select.value, productsPerPage)
    .then(count => {
        paginationState.maxOfPages = count;

        document.getElementById("totalProducts").textContent = paginationState.maxOfPages;
        document.getElementById("currentPage").textContent = paginationState.pageCounter;
    });
    
    sendToServer(1, productsPerPage, filter, type, name);
});


leftButton.addEventListener("click", () => leftButtonHandler(paginationState,productsContainer,innerFilter,productsPerPageSelector));
rightButton.addEventListener("click", () => rightButtonHandler(paginationState,productsContainer,innerFilter,productsPerPageSelector));



filterButton.addEventListener("click", () => HandleFilterButton(filterContainer));




