function financesEditModel() {
    this.options = {
        backdrop: true,
        keyboard: true,
        backdropClick: false,
    };

    this.new = {};
    this.onSaving = null;
    this.onOpen = null;
    this.isOpen = false;
    this.isEdit = false;
    this.isSubmitted = false;

    this.open = function (index, item) {
        this.new = { date: new Date() };

        if (this.onOpen)
            this.onOpen(index, item);

        if (item) {
            this.new = item;
            this.index = index;
            this.isEdit = true;
        }

        this.isOpen = true;
        this.isSubmitted = false;
    };

    this.close = function () {
        this.new = {};

        this.isOpen = false;
        this.isEdit = false;
    };

    this.save = function () {
        if (this.onSaving)
            this.onSaving(this);
    };
}