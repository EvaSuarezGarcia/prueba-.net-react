import CardForm, { CardFormInput } from "./CardForm";
import { InfoCardData } from "../CardList/InfoCard/InfoCard";
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import React from "react";
import * as Constants from "../../Constants";

export interface CardFormDialogProps {
    open: boolean;
    callback: (card: InfoCardData) => void;
    handleClose: () => void;
    dialogTitle: string;
    dialogButton: string;
    initialInput: CardFormInput;
}

const CardFormDialog: React.FC<CardFormDialogProps> = ({
    open,
    callback,
    handleClose: externalHandleClose,
    dialogTitle,
    dialogButton,
    initialInput,
}) => {
    // Form state and handlers
    const [input, setInput] = React.useState<CardFormInput>({
        cardData: initialInput.cardData,
        titleError: initialInput.titleError,
        descriptionError: initialInput.descriptionError,
    });

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ): void => {
        setInput({
            ...input,
            cardData: { ...input.cardData, [e.target.name]: e.target.value },
        });
    };

    const handleClickSubmit = (): void => {
        if (!input.cardData.title || !input.cardData.description) {
            setInput({
                ...input,
                titleError: !input.cardData.title,
                descriptionError: !input.cardData.description,
            });
        } else {
            callback({
                title: input.cardData.title,
                description: input.cardData.description,
                image: input.cardData.image || Constants.DEFAULT_IMAGE_URL,
                key: input.cardData.key || new Date().getTime(),
            });
            externalHandleClose();
        }
    };

    const handleClose = () => {
        setInput({
            cardData: initialInput.cardData,
            titleError: initialInput.titleError,
            descriptionError: initialInput.descriptionError,
        });
        externalHandleClose();
    };

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>{dialogTitle}</DialogTitle>
            <DialogContent>
                <CardForm input={input} handleChange={handleChange} />
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClickSubmit}>{dialogButton}</Button>
            </DialogActions>
        </Dialog>
    );
};

export default CardFormDialog;
