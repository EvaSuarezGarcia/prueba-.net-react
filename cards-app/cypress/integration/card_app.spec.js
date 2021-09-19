describe("Card App", () => {
    beforeEach(() => {
        cy.visit("http://localhost:3000");
    });

    it("frontpage can be opened", () => {
        cy.get("[aria-label='add']");
    });

    it("can create a card without image", () => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type("Test");
        cy.get("textarea[name='description']").type("Description test");
        cy.contains("Añadir").click();
        cy.contains("Test").parent("div").siblings("img").should("have.attr", "src").should("include", "gato-marron");
    });

    const img = "https://es.himgs.com/imagenes/estar-bien/20210217184541/gatos-gestos-lenguaje-significado/0-922-380/gatos-gestos-m.jpg";

    it("can create a card with image", () => {
        cy.get("[aria-label='add']").click();
        cy.get("[name='title']").type("Test 2");
        cy.get("textarea[name='description']").type("Description test 2");
        cy.get("[name='image']").type(img);
        cy.contains("Añadir").click();
        cy.contains("Test 2").parent("div").siblings("img").should("have.attr", "src").should("include", img);
    });

    describe("when a card exists", () => {
        const title = "Test";
        const description = "Description test";

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
    });
});