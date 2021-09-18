import React from "react";
import { FC } from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import AddIcon from "@mui/icons-material/Add";
import { Fab } from "@mui/material";
import { State as AppState } from "../App";
import CardForm from "./CardForm";
import * as Constants from "../../Constants";

interface Props {
    cards: AppState["cards"];
    setCards: React.Dispatch<React.SetStateAction<AppState["cards"]>>;
}

export interface InputState {
    title: string;
    titleError: boolean;
    description: string;
    descriptionError: boolean;
    image: string;
}

const CardFormDialog: FC<Props> = ({ cards, setCards }) => {
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

    const handleSubmit = (): void => {
        if (!input.title) {
            setInput({
                ...input,
                titleError: !input.title,
                descriptionError: !input.description,
            });
        } else {
            setCards([
                ...cards,
                {
                    title: input.title,
                    description: input.description,
                    image: input.image || Constants.DEFAULT_IMAGE_URL,
                },
            ]);
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
            <Fab
                sx={{
                    position: "fixed",
                    bottom: 30,
                    right: 30,
                }}
                color="primary"
                aria-label="add"
                onClick={handleClickOpen}
            >
                <AddIcon />
            </Fab>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Nueva tarjeta</DialogTitle>
                <DialogContent>
                    <CardForm input={input} handleChange={handleChange} />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleSubmit}>Añadir</Button>
                </DialogActions>
            </Dialog>
        </>
    );
};

export default CardFormDialog;
