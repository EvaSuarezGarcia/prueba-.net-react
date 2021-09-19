describe("Card App", () => {
    const title = "Test";
    const description = "Description test";
    const img = "https://ichef.bbci.co.uk/news/640/cpsprodpb/10E9B/production/_109757296_gettyimages-1128004359.jpg";

    beforeEach(() => {
        cy.visit("http://localhost:3000");
    });

    it("frontpage can be opened", () => {
        cy.get("[aria-label='add']");
    });

    it("can create a card without image", () => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type(title);
        cy.get("textarea[name='description']").type(description);
        cy.contains("Añadir").click();
        cy.contains(title).parent("div").siblings("img").should("have.attr", "src").should("include", "gato-marron");
    });

    it("can create a card with image", () => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type(title);
        cy.get("textarea[name='description']").type(description);
        cy.get("[name='image']").type(img);
        cy.contains("Añadir").click();
        cy.contains(title).parent("div").siblings("img").should("have.attr", "src").should("include", img);
    });

    describe("when a card exists", () => {
        beforeEach(() => {
            cy.get("[aria-label='add']").click();
            cy.get("[name='title']").type(title);
            cy.get("textarea[name='description']").type(description);
            cy.contains("Añadir").click();
            cy.contains(title).trigger("mouseover");
        });

        it("a card has actions", () => {
            cy.get("[aria-label='edit']");
            cy.get("[aria-label='delete']");
        });

        it("can edit card", () => {
            const titleEdit = "Test Edit";
            const descriptionEdit = "Description test edit";

            cy.get("[aria-label='edit']").click();
            cy.get("[name='title']").clear().type(titleEdit);
            cy.get("textarea[name='description']").clear().type(descriptionEdit);
            cy.get("[name='image']").clear().type(img);
            cy.get("button").contains("Editar").click();
            cy.contains(titleEdit);
            cy.contains(descriptionEdit);
            cy.contains(titleEdit).parent("div").siblings("img").should("have.attr", "src").should("include", img);
        });

        it("can delete card", () => {
            cy.get("[aria-label='delete']").click();
            cy.get("button").contains("Eliminar").click();
            cy.contains(title).should("not.exist");
        });

        it("card is saved", () => {
            cy.reload();
            cy.contains(title);
        });
    });
});