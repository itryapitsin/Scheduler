function financesChangesModel() {
    this.options = {
        backdrop: true,
        keyboard: true,
        backdropClick: false,
    };

    this.isOpen = false;

    this.open = function () {
        this.isOpen = true;
    };

    this.close = function () {
        this.isOpen = false;
    };
}