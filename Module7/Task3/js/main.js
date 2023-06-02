function  makeItemsSelectable() {
    let itemPanels = document.getElementsByClassName('item_panel');
    for (let itemPanel of itemPanels) {
        let items = itemPanel.getElementsByClassName('item');
        for (let item of items) {
            item.classList.add("selected");
            item.addEventListener("click", function () { item.classList.toggle("selected"); }, false);
        }
    }
}

function moveAllItems(sourceList, destinationList) {
    let items = sourceList.children;
    while (items.length > 0) {
        let item = items[0];
        item.classList.remove('selected');
        destinationList.appendChild(item);
    }
}

function moveSelectedItems(sourceList, destinationList) {
    let items = sourceList.getElementsByClassName('selected');
    if (items.length == 0) {
        alert('nothing is selected');
    }
    while (items.length > 0) {
        let item = items[0];
        item.classList.remove('selected');
        destinationList.appendChild(item);
    }
}

function moveAllButtonAttachClickEvent(button, sourceList, destinationList) {
    button.addEventListener("click", function () { moveAllItems(sourceList, destinationList); }, false);
}

function moveSelectedButtonAttachClickEvent(button, sourceList, destinationList) {
    button.addEventListener("click", function () { moveSelectedItems(sourceList, destinationList); }, false);
}


makeItemsSelectable();
moveAllButtonAttachClickEvent(
    document.getElementById('left_item_list_move_all'),
    document.getElementById('left_item_list'),
    document.getElementById('right_item_list'));
moveAllButtonAttachClickEvent(
    document.getElementById('right_item_list_move_all'),
    document.getElementById('right_item_list'),
    document.getElementById('left_item_list'));
moveSelectedButtonAttachClickEvent(
    document.getElementById('left_item_list_move_selected'),
    document.getElementById('left_item_list'),
    document.getElementById('right_item_list'));
moveSelectedButtonAttachClickEvent(
    document.getElementById('right_item_list_move_selected'),
    document.getElementById('right_item_list'),
    document.getElementById('left_item_list'));