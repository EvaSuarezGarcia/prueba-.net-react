import React from "react";
import { FC } from "react";
import CardForm, { InputProps as InputState } from "./CardForm";
import { Props as InfoCardProps } from "../CardList/InfoCard/InfoCard";
import * as Constants from "../../Constants";
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";

interface Props {
    callbackCard: (card: InfoCardProps) => void;
    dialogOpenButton: React.ReactElement;
}

const CardFormDialog: FC<Props> = ({ callbackCard, dialogOpenButton }) => {
    // Form state and handlers
    const [input, setInput] = React.useState<InputState>({
        title: "",
        titleError: false,
        description: "",
        descriptionError: false,
        image: "",
    });

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ): void => {
        setInput({ ...input, [e.target.name]: e.target.value });
    };

    const handleClickSubmit = (): void => {
        if (!input.title) {
            setInput({
                ...input,
                titleError: !input.title,
                descriptionError: !input.description,
            });
        } else {
            callbackCard({
                title: input.title,
                description: input.description,
                image: input.image || Constants.DEFAULT_IMAGE_URL,
                key: new Date().getTime(),
            });
            handleClose();
        }
    };

    // Modal state and handlers
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = (): void => {
        setOpen(true);
    };

    const handleClose = (): void => {
        setOpen(false);
        setInput({
            title: "",
            titleError: false,
            description: "",
            descriptionError: false,
            image: "",
        });
    };

    return (
        <>
            {React.cloneElement(dialogOpenButton, { onClick: handleClickOpen })}
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Nueva tarjeta</DialogTitle>
                <DialogContent>
                    <CardForm input={input} handleChange={handleChange} />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClickSubmit}>Añadir</Button>
                </DialogActions>
            </Dialog>
        </>
    );
};

export default CardFormDialog;
